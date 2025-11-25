using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Game;

[ContentImporter(".game", DisplayName = "Game Record Importer", DefaultProcessor = "GameRecordProcessor")]
public sealed class GameRecordImporter : JsonImporter<GameRecordDto>
{
}
