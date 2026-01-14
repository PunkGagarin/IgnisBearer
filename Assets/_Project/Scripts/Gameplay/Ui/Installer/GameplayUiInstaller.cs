using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Installer
{
    public class GameplayUiInstaller : MonoInstaller
    {

        [field: SerializeField]
        public Camera UiCamera { get; private set; }
        
        [field: SerializeField]
        public GameplayUiRoot GameplayUiRoot { get; private set; }

        [field: SerializeField]
        public BarrierUi BarrierUi { get; private set; }

        [field: SerializeField]
        public FateUi FateUi { get; private set; }

        [field: SerializeField]
        public GameEndUI GameEndUI { get; private set; }
        
        [field: SerializeField]
        public UiSettings UiSettings { get; private set; }

        public override void InstallBindings()
        {
            Container.Bind<Camera>()
                .FromInstance(UiCamera)
                .AsSingle();

            Container.Bind<GameplayUiRoot>()
                .FromInstance(GameplayUiRoot)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BarrierUi>()
                .FromInstance(BarrierUi)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<FateUi>()
                .FromInstance(FateUi)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<GameEndUI>()
                .FromInstance(GameEndUI)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<UiSettings>()
                .FromInstance(UiSettings)
                .AsSingle();
        }
    }
}