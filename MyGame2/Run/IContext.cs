namespace BeeCreak.Run;
public interface IContext
{
	StaticContext Static { get; set; }
	DynamicContext Dynamic { get; set; }
}