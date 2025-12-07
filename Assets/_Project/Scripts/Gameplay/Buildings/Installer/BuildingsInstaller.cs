using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
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
        private AutoHarvestSettings _autoHarvestSettings;

        [field: SerializeField]
        private AutoLighterSettings _autoLighterSettings;
        
        [field: SerializeField]
        private BuildingSlotsSettings _buildingSlotsSettings;

        [field: SerializeField]
        private LanternSettings _lanternSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BuildingSlotsSettings>().FromInstance(_buildingSlotsSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<ChurchSettings>().FromInstance(_churchSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<FactorySettings>().FromInstance(_factorySettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoHarvestSettings>().FromInstance(_autoHarvestSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoLighterSettings>().FromInstance(_autoLighterSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<HouseSettings>().FromInstance(_houseSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingSlotsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingAddingOptionsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<LanternSettings>().FromInstance(_lanternSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LanternFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternService>().AsSingle();
        }
    }

}