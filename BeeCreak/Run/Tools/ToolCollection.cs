using BeeCreak.Run.Tools.Dynamic;
using BeeCreak.Run.Tools.Static;

namespace BeeCreak.Run.Tools;

public class ToolCollection : IToolCollection
{
    public IStaticToolCollection Static { get; set; } = new StaticTools();
    public IDynamicToolCollection Dynamic { get; set; } = new DynamicTools();
}
