namespace BeeCreak.Game.Scene
{
    using global::BeeCreak.Game.Objects.Camera;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class CellManager
    {
        public CellManager(IToolCollection tools, ICell cell)
        {
            Tools = tools;

            TileManager = new TileManager(Tools, cell.Tiles, cell.Size);
            LightManager = new LightManager(Tools, cell.Lights, cell.Size);
            EntityManager = new EntityManager(Tools, cell.Entities, cell.Name);
        }

        private TileManager TileManager { get; set; }

        private LightManager LightManager { get; set; }

        private EntityManager EntityManager { get; set; }

        private IToolCollection Tools { get; set; }

        public void Update(GameTime gameTime)
        {
            EntityManager.Update(gameTime);
            LightManager.Update(gameTime);
        }

        public void Draw(ICamera camera)
        {
            Tools.Static.GraphicsDevice.SetRenderTarget(null);

            Tools.Static.Sprite.Batch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                transformMatrix: camera.ZoomTransform);

            TileManager.Draw();
            EntityManager.Draw();

            Tools.Static.Sprite.Batch.End();

            Tools.Static.Sprite.Batch.Begin(
                blendState: Tools.Static.Sprite.Multiply,
                transformMatrix: camera.ZoomTransform);

            LightManager.Draw();

            Tools.Static.Sprite.Batch.End();
        }
    }
}