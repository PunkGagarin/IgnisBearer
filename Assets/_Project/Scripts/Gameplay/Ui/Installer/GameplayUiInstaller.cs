using _Project.Scripts.Gameplay.Buildings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Installer
{
    public class GameplayUiInstaller : MonoInstaller
    {

        [field: SerializeField]
        public GameplayUiRoot GameplayUiRoot { get; private set; }

        [field: SerializeField]
        public BarrierUi BarrierUi { get; private set; }

        [field: SerializeField]
        public FateUi FateUi { get; private set; }

        public override void InstallBindings()
        {
            Container.Bind<GameplayUiRoot>()
                .FromInstance(GameplayUiRoot)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BarrierUi>()
                .FromInstance(BarrierUi)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<FateUi>()
                .FromInstance(FateUi)
                .AsSingle();
        }
    }
}