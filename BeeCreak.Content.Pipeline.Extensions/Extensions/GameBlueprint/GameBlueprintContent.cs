using System.Collections.Generic;

namespace BeeCreak.Content.Pipeline.Extensions;

public class GameBlueprintContent
{
    public string ActiveCellId { get; set; } = string.Empty;
    
    public List<string> Cells { get; set; } = new();
}
