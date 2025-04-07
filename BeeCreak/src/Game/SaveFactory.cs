using System.Collections.Generic;
using System.Linq;

public class SaveFactory
{
    private readonly CellFactory cellFactory;

    public SaveFactory(CellFactory cellFactory)
    {
        this.cellFactory = cellFactory;
    }

    public GameState fromDTO(GameStateDTO gameState)
    {
        return new GameState()
        {
            ActiveCell = gameState.ActiveCell,
            Cells = gameState.Cells.Select(cell => new KeyValuePair<string, CellState>(cell.Key, cellFactory.fromDTO(cell.Value)))
                .ToDictionary(cell => cell.Key, cell => cell.Value)
        };
    }

    public GameStateDTO toDTO(GameState gameState)
    {
        return new GameStateDTO()
        {
            ActiveCell = gameState.ActiveCell,
            Cells = gameState.Cells.Select(cell => new KeyValuePair<string, CellStateDTO>(cell.Key, cellFactory.toDTO(cell.Value)))
                .ToDictionary(cell => cell.Key, cell => cell.Value)
        };
    }
}