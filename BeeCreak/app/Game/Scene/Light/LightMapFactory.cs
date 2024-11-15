namespace BeeCreak.Game.Scene.Light
{
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework.Graphics;

    public class LightMapFactory
    {
        private readonly ISprite sprite;

        public LightMapFactory(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public static LightMapDTO CreateLightMapDTO(ILightMap lightMap)
        {
            var lights = lightMap.Lights;
            var size = lightMap.Lights.GetLength(0);

            var lightDTOArray = new LightDTO[size, size];

            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var light = lights[x, y];

                    lightDTOArray[x, y] = new LightDTO { Radius = light.Radius, Period = light.Period, Scale = light.Scale, MaxScale = light.MaxScale };
                }
            }

            return new LightMapDTO(lightDTOArray);
        }

        public ILightMap CreateLightMap(LightMapDTO lightMap)
        {
            var lights = lightMap.Lights;
            var size = lights.GetLength(0);

            var lightArray = new ILight[size, size];

            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var lightDTO = lights[x, y];

                    var light = new Light(sprite) { Radius = lightDTO.Radius, Period = lightDTO.Period, Scale = lightDTO.Scale, MaxScale = lightDTO.MaxScale };

                    lightArray[x, y] = light;
                }
            }

            return new LightMap(lightArray);
        }
    }
}