using Microsoft.Xna.Framework.Content;

public class CellAttributesReader : ContentTypeReader<CellAttributes>
{
    protected override CellAttributes Read(ContentReader input, CellAttributes existingInstance)
    {
        var tint = input.ReadColor();
        var lengthOfDay = input.ReadInt32();
        var lengthOfNight = input.ReadInt32();

        var tileMapWidth = input.ReadInt32();
        var tileMapHeight = input.ReadInt32();
        var tileMap = new Grid<TileState>(tileMapWidth, tileMapHeight);

        for (int y = 0; y < tileMapHeight; y++)
        {
            for (int x = 0; x < tileMapWidth; x++)
            {
                tileMap[x, y] = new TileState(input.ReadString());
            }
        }

        return new CellAttributes(tint, lengthOfDay, lengthOfNight, tileMap);
    }
}