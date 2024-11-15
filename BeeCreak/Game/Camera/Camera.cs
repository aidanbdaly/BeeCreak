namespace BeeCreak.Game.Camera
{
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Camera : ICamera
    {
        private readonly ISprite sprite;

        public Camera(ISprite sprite, IEventManager events)
        {
            ViewPortWidth = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            ViewPortHeight = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            events.Listen<FocusOnEvent>(FocusOn);

            Zoom = 3.5f;
        }

        public Matrix ZoomTransform { get; set; }

        public int ViewPortWidth { get; set; }

        public int ViewPortHeight { get; set; }

        public float Zoom { get; set; }

        private Entity Target { get; set; }

        public CameraDTO ToDTO()
        {
            return new CameraDTO
            {
                ZoomTransform = ZoomTransform,
                ViewPortWidth = ViewPortWidth,
                ViewPortHeight = ViewPortHeight,
                Zoom = Zoom,
            };
        }

        public void Update(GameTime gameTime)
        {
            ViewPortWidth = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            ViewPortHeight = sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            var worldPosition =
                Target.WorldPosition
                + new Vector2(16, 16)
                - new Vector2(ViewPortWidth / 2, ViewPortHeight / 2);

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.OemPlus))
            {
                Zoom += 0.01f;
            }

            if (keyboardState.IsKeyDown(Keys.OemMinus))
            {
                Zoom -= 0.01f;
            }

            GetTransform(worldPosition);
        }

        private void FocusOn(FocusOnEvent focusOnEvent)
        {
            Target = focusOnEvent.Target;

            GetTransform(focusOnEvent.Target.WorldPosition);
        }

        private void GetTransform(Vector2 worldPosition)
        {
            ZoomTransform =
                Matrix.CreateTranslation(-worldPosition.X, -worldPosition.Y, 0)
                * Matrix.CreateTranslation(-ViewPortWidth / 2, -ViewPortHeight / 2, 0)
                * Matrix.CreateScale(Zoom)
                * Matrix.CreateTranslation(ViewPortWidth / 2, ViewPortHeight / 2, 0);
        }
    }
}