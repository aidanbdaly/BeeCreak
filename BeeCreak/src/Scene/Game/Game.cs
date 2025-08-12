using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{

    public sealed class Game
    {
        public event EventHandler<ActiveCellChangedEventArgs> ActiveCellChanged;

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public GameBlueprint Blueprint { get; init; }

        public GameState State { get; init; }

        public void SetActiveCell(string newCellId)
        {
            if (!Blueprint.Cells.Contains(newCellId))
                throw new ArgumentException($"Cell '{newCellId}' does not exist.");

            if (newCellId == State.ActiveCellId) return;

            State.ActiveCellId = newCellId;

            ActiveCellChanged?.Invoke(this, new ActiveCellChangedEventArgs(State.ActiveCell));
        }

        public void MoveEntity(string entityId, string fromCellId, string toCellId)
        {
            if (!State.CellState.TryGetValue(fromCellId, out var fromCell) ||
                !State.CellState.TryGetValue(toCellId, out var toCell))
                throw new KeyNotFoundException("One of the cells does not exist.");

            if (!fromCell.EntityState.TryGetValue(entityId, out var entity))
                throw new InvalidOperationException($"Entity '{entityId}' not found in cell '{fromCellId}'.");

            toCell.EntityState[entityId] = entity;
            fromCell.EntityState.Remove(entityId);
        }
    }

}
