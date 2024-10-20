using BeeCreak.Tools.Dynamic;
using BeeCreak.Tools.Static;

namespace BeeCreak.Tools;

public class ToolCollection : IToolCollection
{
    public IStaticToolCollection Static { get; set; } = new StaticTools();
    public IDynamicToolCollection Dynamic { get; set; } = new DynamicTools();
}
