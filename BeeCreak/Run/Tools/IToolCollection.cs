using BeeCreak.Run.Tools.Dynamic;
using BeeCreak.Run.Tools.Static;

namespace BeeCreak.Run.Tools;

public interface IToolCollection
{
    IStaticToolCollection Static { get; set; }
    IDynamicToolCollection Dynamic { get; set; }
}
