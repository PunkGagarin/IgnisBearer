using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class PlayerInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerDataService>()
                .AsSingle()
                .NonLazy();
        }
    }
}