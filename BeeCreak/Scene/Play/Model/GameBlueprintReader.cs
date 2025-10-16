using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.src.Models
{
    public class GameBlueprintReader : ContentTypeReader<GameBlueprint>
    {
        protected override GameBlueprint Read(ContentReader input, GameBlueprint existingInstance)
        {
            var activeCellId = input.ReadString();
            
            var cellCount = input.ReadInt32();
            var cells = new List<string>(cellCount);
            
            for (int i = 0; i < cellCount; i++)
            {
                cells.Add(input.ReadString());
            }

            return new GameBlueprint
            {
                ActiveCellId = activeCellId,
                Cells = cells
            };
        }
    }
}
