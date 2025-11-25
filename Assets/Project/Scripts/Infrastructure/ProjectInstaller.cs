using Project.Scripts.Infrastructure.GameStates;
using Project.Scripts.Infrastructure.GameStates.States;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure
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
            Container.BindInterfacesAndSelfTo<LoadSceneState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        }
    }

}