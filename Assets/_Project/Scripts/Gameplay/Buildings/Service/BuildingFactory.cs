using System;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingFactory
    {
        [Inject] private readonly DiContainer _container;

        [Inject] private readonly ChurchSettings _churchSettings;
        [Inject] private readonly BuildingSlotsSettings _buildingSlotsSettings;
        [Inject] private readonly HouseSettings _houseSettings;
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoHarvestSettings _autoHarvestSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private BuildingComponentsInitService _buildingComponentsInitService;

        public BuildingSlot CreateSlotAtPosition(BuildingSlotsSpawnPoint buildingSlotsSpawnPoint)
        {
            var slot = _container.InstantiatePrefabForComponent<BuildingSlot>(_buildingSlotsSettings.Prefab,
                buildingSlotsSpawnPoint.transform.position, Quaternion.identity, null);
            return slot;
        }

        public Building BuildByType(BuildingType buildingType, BuildingSlot slot)
        {
            return buildingType switch
            {
                BuildingType.Church => BuildChurch(slot),
                BuildingType.House => BuildHouse(slot),
                BuildingType.Factory => BuildFactory(slot),
                BuildingType.AutoHarvest => BuildAutoHarvester(slot),
                BuildingType.AutoLighter => BuildAutoLighter(slot),
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
        }

        private ChurchBuilding BuildChurch(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<ChurchBuilding>(slot, _churchSettings.Prefab);
            var church = (ChurchBuilding)_buildingComponentsInitService.InitBuildingComponents(building);
            slot.SetEnabled(false);
            return church;
        }

        private HouseBuilding BuildHouse(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<HouseBuilding>(slot, _houseSettings.Prefab);
            var house = (HouseBuilding)_buildingComponentsInitService.InitBuildingComponents(building);
            slot.SetEnabled(false);
            return house;
        }

        private AutoLighterBuilding BuildAutoLighter(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<AutoLighterBuilding>(slot, _autoLighterSettings.Prefab);
            var autoLighter = (AutoLighterBuilding)_buildingComponentsInitService.InitBuildingComponents(building);
            slot.SetEnabled(false);
            return autoLighter;
        }

        private AutoHarvestBuilding BuildAutoHarvester(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<AutoHarvestBuilding>(slot, _autoHarvestSettings.Prefab);
            var harvester = (AutoHarvestBuilding)_buildingComponentsInitService.InitBuildingComponents(building);
            slot.SetEnabled(false);
            return harvester;
        }


        private FactoryBuilding BuildFactory(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<FactoryBuilding>(slot, _factorySettings.Prefab);
            var factory = (FactoryBuilding)_buildingComponentsInitService.InitBuildingComponents(building);
            slot.SetEnabled(false);
            return factory;
        }

        private T InstantiateBuildingOnSlot<T>(BuildingSlot slot, Building prefab) where T : Building
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<T>(prefab,
                    parentTransform.position, Quaternion.identity, parentTransform);
            return building;
        }
        
    }
}