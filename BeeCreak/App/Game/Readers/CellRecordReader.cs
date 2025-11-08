using BeeCreak.App.Game.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.App.Game.Readers;

public sealed class CellRecordReader : ContentTypeReader<CellRecord>
{
    protected override CellRecord Read(ContentReader input, CellRecord existingInstance)
    {
        var id = input.ReadString();
        return new CellRecord(id);
    }
}
