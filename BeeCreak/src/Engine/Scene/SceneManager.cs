using Microsoft.Extensions.DependencyInjection;

namespace BeeCreak
{
    public class SceneManager
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public SceneManager(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public IScene Scene { get; set; }

        private IServiceScope Scope { get; set; }

        public void ChangeScene<TScene>() where TScene : IScene
        {
            if (Scene != null && Scope != null)
            {
                Scene.Exit(() =>
                {
                    Scope.Dispose();
                
                    Scope = serviceScopeFactory.CreateScope();
                    Scene = Scope.ServiceProvider.GetRequiredService<TScene>();
                    Scene.Enter();
                });
            }
            else
            {
                Scope = serviceScopeFactory.CreateScope();
                Scene = Scope.ServiceProvider.GetRequiredService<TScene>();
                Scene.Enter();
            }
        }
    }
}