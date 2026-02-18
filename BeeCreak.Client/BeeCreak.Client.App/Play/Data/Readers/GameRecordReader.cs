using BeeCreak.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Readers;

public sealed class GameRecordReader : ContentTypeReader<GameRecord>
{
    protected override GameRecord Read(ContentReader input, GameRecord existingInstance)
    {
        var activeCell = input.ReadObject<CellReference>();
        return new GameRecord(activeCell);
    }
}
