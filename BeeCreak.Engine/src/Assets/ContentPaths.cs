namespace BeeCreak.Engine.Assets
{
    /// <summary>
    /// Centralized registry of all content asset paths used throughout the game.
    /// </summary>
    public static class ContentPaths
    {
        // Game
        public const string GameBlueprint = "Game/game";

        // UI
        public const string FontLookout = "UI/lookout";
        public const string SpriteSheetButtons = "UI/buttons";

        // Tiles
        public const string TileCatalogue = "Tiles/catalogue";
        public const string SpriteSheetTiles = "Tiles/tiles";

        // Entities
        public const string SpriteSheetEntities = "Entities/entities";

        // Dynamic Paths (use these helper methods for content loaded by ID)
        public static string CellBlueprint(string cellId) => $"Cells/Blueprints/{cellId}";
        public static string CellAttributes(string cellId) => $"Cells/Attributes/{cellId}";
        public static string EntityAttributes(string contentId) => $"Entities/{contentId}";
    }
}
