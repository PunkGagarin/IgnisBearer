using Project.Scripts.SceneManagement;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.SceneManagement
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();
            Container.Bind<LoadingCurtain>().AsSingle();
        }
    }
}
