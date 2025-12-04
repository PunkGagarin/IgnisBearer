using _Project.Scripts.Gameplay;
using _Project.Scripts.Infrastructure.GameStates.States;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {

        [field: SerializeField]
        public LevelData LevelData { get; private set; }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
            Container.Bind<LevelData>().FromInstance(LevelData).AsSingle();

            Container.BindInterfacesAndSelfTo<GameplayBootstrap>().AsSingle().NonLazy();
        }
    }
}