namespace BeeCreak.Game.Scene.Light
{
    public class LightMap : ILightMap
    {
        public LightMap(ILight[,] lights)
        {
            Lights = lights;
        }

        public ILight[,] Lights { get; set; }
    }
}