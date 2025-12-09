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
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<ChurchBuilding>(_churchSettings.ChurchBuildingPrefab,
                    parentTransform.position, Quaternion.identity, parentTransform);

            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(0, _churchSettings.MaxGrade, _churchSettings.GradePrice);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, _churchSettings.MaxUnitsCount);
            
            building.TryGetComponent<ILightStorage>(out var lightStorage);
            lightStorage.Init(_churchSettings.MaxLightStorageCapacity);

            building.TryGetComponent<IFateProducer>(out var fateGenerator);
            fateGenerator.Init(_churchSettings.TimeToProduceFate, _churchSettings.AmountToProduceFateAtTime);
            
            building.TryGetComponent<IFateStorage>(out var fateStorage);
            fateStorage.Init(_churchSettings.MaxFateStorageCapacity);
            
            slot.SetEnabled(false);

            return building;
        }


        //todo: вынести общий код?
        private HouseBuilding BuildHouse(BuildingSlot slot)
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<HouseBuilding>(_houseSettings.HouseBuildingPrefab,
                    parentTransform.position, Quaternion.identity, parentTransform);

            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(0, _houseSettings.MaxGrade, _houseSettings.GradePrice);

            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(_houseSettings.MaxDurability, _houseSettings.MaxDurability);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, _houseSettings.MaxUnitsCount);

            slot.SetEnabled(false);

            return building;
        }

        private AutoLighterBuilding BuildAutoLighter(BuildingSlot slot)
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<AutoLighterBuilding>(
                    _autoLighterSettings.AutoLighterBuildingPrefab,
                    parentTransform.position, Quaternion.identity, parentTransform);

            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(0, _autoLighterSettings.MaxGrade, _autoLighterSettings.GradePrice);

            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(_autoLighterSettings.MaxDurability, _autoLighterSettings.MaxDurability);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, _houseSettings.MaxUnitsCount);

            building.Init();

            slot.SetEnabled(false);

            return building;
        }

        private AutoHarvestBuilding BuildAutoHarvester(BuildingSlot slot)
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<AutoHarvestBuilding>(
                    _autoHarvestSettings.AutoHarvestBuildingPrefab,
                    parentTransform.position, Quaternion.identity, parentTransform);

            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(0, _autoHarvestSettings.MaxGrade, _autoHarvestSettings.GradePrice);

            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(_autoHarvestSettings.MaxDurability, _autoHarvestSettings.MaxDurability);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, _houseSettings.MaxUnitsCount);

            building.Init();

            slot.SetEnabled(false);

            return building;
        }


        private FactoryBuilding BuildFactory(BuildingSlot slot)
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<FactoryBuilding>(_factorySettings.FactoryBuildingPrefab,
                    parentTransform.position, Quaternion.identity, parentTransform);

            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(0, _factorySettings.MaxGrade, _factorySettings.GradePrice);

            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(_factorySettings.MaxDurability, _factorySettings.MaxDurability);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, _houseSettings.MaxUnitsCount);

            building.Init();

            slot.SetEnabled(false);

            return building;
        }
    }
}