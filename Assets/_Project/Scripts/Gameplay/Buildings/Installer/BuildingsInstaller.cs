using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using UnityEngine;
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
        private FateGeneratorSettings FateGeneratorSettings;

        [field: SerializeField]
        private BuildingSlotsSettings _buildingSlotsSettings;

        [field: SerializeField]
        private LanternSettings _lanternSettings;
        

        [field: SerializeField]
        private LightConsumeSettings LightConsumeSettings { get; set; }

        public override void InstallBindings()
        {
            BindMainBuildings();
            BindSlots();
            BindChurchWithLightAndFate();
            BindLanterns();
            BindBuildingSettings();
        }

        private void BindBuildingSettings()
        {
            Container.BindInterfacesAndSelfTo<FactorySettings>().FromInstance(_factorySettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoHarvestSettings>().FromInstance(_autoHarvestSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoLighterSettings>().FromInstance(_autoLighterSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<HouseSettings>().FromInstance(_houseSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<FateGeneratorSettings>().FromInstance(FateGeneratorSettings).AsSingle();
        }

        private void BindMainBuildings()
        {
            Container.BindInterfacesAndSelfTo<BuildingFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingAddingOptionsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingComponentsInitService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingComponentsUpdateService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsService>().AsSingle();
        }

        private void BindSlots()
        {
            Container.BindInterfacesAndSelfTo<BuildingSlotsSettings>().FromInstance(_buildingSlotsSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingSlotsService>().AsSingle();
        }

        private void BindChurchWithLightAndFate()
        {
            Container.BindInterfacesAndSelfTo<ChurchSettings>().FromInstance(_churchSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LightConsumeSettings>().FromInstance(LightConsumeSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<FateService>().AsSingle();
        }

        private void BindLanterns()
        {
            Container.BindInterfacesAndSelfTo<LanternSettings>().FromInstance(_lanternSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LanternFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternSlotsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternService>().AsSingle();
        }

    }

}