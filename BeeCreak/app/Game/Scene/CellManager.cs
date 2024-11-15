namespace BeeCreak.Game.Scene
{
    using global::BeeCreak.Game.Camera;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.Scene.Tile;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class CellManager
    {
        private readonly ISprite sprite;

        public CellManager(ISprite sprite, TileManager tileManager, LightManager lightManager, EntityManager entityManager)
        {
            this.sprite = sprite;

            TileManager = tileManager;
            LightManager = lightManager;
            EntityManager = entityManager;
        }

        private TileManager TileManager { get; set; }

        private LightManager LightManager { get; set; }

        private EntityManager EntityManager { get; set; }

        public void SetActiveCell(ICell cell)
        {
            TileManager.SetTileMap(cell.TileMap);
            LightManager.SetLightMap(cell.LightMap);
            EntityManager.SetEntities(cell.Entities);
        }

        public void Update(GameTime gameTime)
        {
            EntityManager.Update(gameTime);
            LightManager.Update(gameTime);
        }

        public void Draw(ICamera camera)
        {
            sprite.GraphicsDevice.SetRenderTarget(null);

            sprite.Batch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                transformMatrix: camera.ZoomTransform);

            TileManager.Draw();
            EntityManager.Draw();

            sprite.Batch.End();

            LightManager.Draw(camera);
        }
    }
}