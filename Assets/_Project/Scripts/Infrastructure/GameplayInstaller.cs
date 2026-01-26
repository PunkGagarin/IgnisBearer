using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Level;
using _Project.Scripts.Gameplay.Ui;
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
            Container.BindInterfacesAndSelfTo<PopupController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEndService>().AsSingle();
            Container.BindInterfacesAndSelfTo<MetaCurrencyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<StartDataFactory>().AsSingle();
            
            Container.Bind<LevelData>().FromInstance(LevelData).AsSingle();

            Container.BindInterfacesAndSelfTo<GameplayBootstrap>().AsSingle().NonLazy();
        }
    }
}