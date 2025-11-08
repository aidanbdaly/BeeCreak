using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.CellReference;

[ContentProcessor(DisplayName = "Cell Reference Processor")]
public sealed class CellReferenceProcessor : ContentProcessor<CellReferenceDto, CellReferenceContent>
{
    public override CellReferenceContent Process(CellReferenceDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Cell reference payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Cell reference requires an id.");
        }

        if (string.IsNullOrWhiteSpace(input.Base))
        {
            throw new InvalidContentException($"Cell reference '{input.Id}' requires a base cell id.");
        }

        if (string.IsNullOrWhiteSpace(input.TileMap))
        {
            throw new InvalidContentException($"Cell reference '{input.Id}' requires a tile map id.");
        }

        var content = new CellReferenceContent
        {
            Id = input.Id,
            BaseCellId = input.Base,
            TileMapId = input.TileMap
        };

        if (input.Entities is not null)
        {
            foreach (var entityId in input.Entities)
            {
                if (!string.IsNullOrWhiteSpace(entityId))
                {
                    content.EntityReferenceIds.Add(entityId);
                }
            }
        }

        return content;
    }
}
