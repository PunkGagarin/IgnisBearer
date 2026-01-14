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
        private FateGeneratorSettings FateGeneratorSettings { get; set; }

        [field: SerializeField]
        private BuildingSlotsSettings _buildingSlotsSettings;

        [field: SerializeField]
        private BuildingSettings BuildingSettings { get; set; }

        [field: SerializeField]
        private LanternSettings _lanternSettings;


        [field: SerializeField]
        private LightConsumeSettings LightConsumeSettings { get; set; }

        public override void InstallBindings()
        {
            BindMainBuildings();
            BindSlots();
            BindServices();
            BindLanterns();
            BindBuildingSettings();
        }

        private void BindBuildingSettings()
        {
            Container.BindInterfacesAndSelfTo<BuildingSettings>().FromInstance(BuildingSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<FactorySettings>().FromInstance(_factorySettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoHarvestSettings>().FromInstance(_autoHarvestSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoLighterSettings>().FromInstance(_autoLighterSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<HouseSettings>().FromInstance(_houseSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<FateGeneratorSettings>().FromInstance(FateGeneratorSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<ChurchSettings>().FromInstance(_churchSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LanternSettings>().FromInstance(_lanternSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<LightConsumeSettings>().FromInstance(LightConsumeSettings).AsSingle();
        }

        private void BindMainBuildings()
        {
            Container.BindInterfacesAndSelfTo<AutoHarvesterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AutoLighterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChurchFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<HouseFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<FactoryBuildingFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<FateGeneratorFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingAddingOptionsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsService>().AsSingle();
        }

        private void BindSlots()
        {
            Container.BindInterfacesAndSelfTo<BuildingSlotEnabler>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingSlotsFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingSlotsSettings>().FromInstance(_buildingSlotsSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingSlotsService>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<FateService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LightResourceService>().AsSingle();
        }

        private void BindLanterns()
        {
            Container.BindInterfacesAndSelfTo<LanternFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternSlotsService>().AsSingle();
        }
    }
}