using System;
using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;
using BeeCreak.Play.State;
using Microsoft.Xna.Framework;

namespace BeeCreak.Play.Scenes
{
    public sealed class PlayScene : Scene
    {
        private const int DefaultWidth = 800;
        private const int DefaultHeight = 600;
        private const string DefaultSaveId = "default";
        private const string DefaultFontId = "UI/lookout";

        private readonly Context context;
        private readonly BaseComponentFactory componentFactory;

        private GameState gameState;

        public PlayScene(Context context)
        {
            this.context = context;

            Width = DefaultWidth;
            Height = DefaultHeight;

            componentFactory = new BaseComponentFactory(context);
        }

        public override void LoadContent()
        {
            var saveId = string.IsNullOrWhiteSpace(context.saveId) ? DefaultSaveId : context.saveId;
            gameState = stateRepository.LoadOrCreate(saveId);

            var label = componentFactory.Text($"Active Cell: {gameState.ActiveCellId}", DefaultFontId);
            label.Position = new Vector2(Width / 2f, Height / 2f);
            AddComponent(label);
        }

        public override void Dispose()
        {
            if (gameState != null)
            {
                var saveId = string.IsNullOrWhiteSpace(context.saveId) ? DefaultSaveId : context.saveId;
                stateRepository.Save(saveId, gameState);
            }

            base.Dispose();
        }
    }
}
