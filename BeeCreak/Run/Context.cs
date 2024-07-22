namespace BeeCreak.Run;
public class Context : IContext
{
	public StaticContext Static { get; set; } = default!;
	public DynamicContext Dynamic { get; set; } = default!;
}