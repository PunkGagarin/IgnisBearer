using Zenject;

namespace Project.Scripts.SceneManagement
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();
        }
    }
}
