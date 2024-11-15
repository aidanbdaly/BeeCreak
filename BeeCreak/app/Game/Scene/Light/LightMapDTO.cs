namespace BeeCreak.Game.Scene.Light
{
    public class LightMapDTO
    {
        public LightMapDTO(LightDTO[,] lights)
        {
            Lights = lights;
        }

        public LightDTO[,] Lights { get; set; }
    }
}