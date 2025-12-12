using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Installer
{
    public class GameplayUiInstaller : MonoInstaller
    {

        [field: SerializeField]
        public GameplayUiRoot GameplayUiRoot { get; private set; }

        public override void InstallBindings()
        {
            Container.Bind<GameplayUiRoot>()
                .FromInstance(GameplayUiRoot)
                .AsSingle();
        }
    }
}