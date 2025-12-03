using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Church;
using _Project.Scripts.Gameplay.Buildings.House;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Installer
{
    public class BuildingsInstaller : MonoInstaller
    {
        [field: SerializeField]
        private ChurchSettings _churchSettings;

        [field: SerializeField]
        private HouseSettings _houseSettings;

        [field: SerializeField]
        private LanternSettings _lanternSettings;
        
        
        [field: SerializeField]
        public List<LanternSpawnPoint> StartLanternPoints { get; private set; }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BuildingFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChurchSettings>().FromInstance(_churchSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<HouseSettings>().FromInstance(_houseSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LanternSettings>().FromInstance(_lanternSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LanternFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternService>().AsSingle();
            Container.Bind<List<LanternSpawnPoint>>().FromInstance(StartLanternPoints).AsSingle();
        }
    }

}