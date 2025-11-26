using _Project.Scripts.Infrastructure.GameStates;
using _Project.Scripts.Infrastructure.GameStates.States;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            BindStateMachine();
        }

        private void BindStateMachine()
        {
            Debug.Log(" BindStateMachine from installer");
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadGameplayState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        }
    }

}