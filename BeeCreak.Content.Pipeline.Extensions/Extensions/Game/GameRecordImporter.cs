using BeeCreak.Content.Pipeline.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.Game;

[ContentImporter(".game", DisplayName = "Game Record Importer", DefaultProcessor = "GameRecordProcessor")]
public sealed class GameRecordImporter : JsonImporter<GameRecordDto>
{
}
