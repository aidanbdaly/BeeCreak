using BeeCreak.Tools.Dynamic;
using BeeCreak.Tools.Static;

namespace BeeCreak.Tools;

public interface IToolCollection
{
    IStaticToolCollection Static { get; set; }
    IDynamicToolCollection Dynamic { get; set; }
}
