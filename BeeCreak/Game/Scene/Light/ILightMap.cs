namespace BeeCreak.Game.Scene.Light
{
    public interface ILightMap
    {
        ILight[,] Lights { get; set; }
    }
}