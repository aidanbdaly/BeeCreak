namespace BeeCreak
{
    public interface IAppRouter
    {
        AppNode CurrentNode { get; set; }

        void SetRoot(AppNode root);

        void Navigate(string path);
    }
}