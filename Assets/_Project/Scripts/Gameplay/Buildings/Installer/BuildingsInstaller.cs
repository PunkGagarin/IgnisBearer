using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Church;
using _Project.Scripts.Gameplay.Buildings.Factory;
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
        private FactorySettings _factorySettings;

        [field: SerializeField]
        private AutoCollectorSettings _autoCollectorSettings;

        [field: SerializeField]
        private AutoLighterSettings _autoLighterSettings;

        [field: SerializeField]
        private LanternSettings _lanternSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BuildingFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChurchSettings>().FromInstance(_churchSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<FactorySettings>().FromInstance(_factorySettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoCollectorSettings>().FromInstance(_autoCollectorSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoLighterSettings>().FromInstance(_autoLighterSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<HouseSettings>().FromInstance(_houseSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<LanternSettings>().FromInstance(_lanternSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LanternFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternService>().AsSingle();
        }
    }

}