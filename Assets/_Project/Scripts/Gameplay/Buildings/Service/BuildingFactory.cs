using System;
using _Project.Scripts.Gameplay.Buildings.AutoCollector;
using _Project.Scripts.Gameplay.Buildings.AutoLighter;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.Durability;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.Grade;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.Workers;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.WorkersCapacity;
using _Project.Scripts.Gameplay.Buildings.Church;
using _Project.Scripts.Gameplay.Buildings.Factory;
using _Project.Scripts.Gameplay.Buildings.House;
using _Project.Scripts.Gameplay.BuildingsSlots;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Service
{
    public class BuildingFactory
    {
        [Inject] private readonly DiContainer _container;

        [Inject] private readonly ChurchSettings _churchSettings;
        [Inject] private readonly BuildingSlotsSettings _buildingSlotsSettings;
        [Inject] private readonly HouseSettings _houseSettings;
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoCollectorSettings _autoCollectorSettings;
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
                BuildingType.AutoCollector => BuildAutoCollector(slot),
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
            specUnits.Init();

            building.TryGetComponent<IWorkersCapacity>(out var capacity);
            capacity.Init(0, _churchSettings.MaxUnitsCount);

            /*
            building.TryGetComponent<IFateGenerator>(out var fateGenerator);
            fateGenerator.Init();
            
            building.TryGetComponent<IFateStorage>(out var fateStorage);
            fateStorage.Init(_churchSettings.MaxFateStorageCapacity);
            
            building.TryGetComponent<IChurchLightStorage>(out var lightStorage);
            lightStorage.Init(_churchSettings.MaxLightStorageCapacity);
            */

            slot.SetEnabled(false);

            return building;
        }


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
            specUnits.Init();

            building.TryGetComponent<IWorkersCapacity>(out var capacity);
            capacity.Init(0, _houseSettings.MaxUnitsCount);

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
            specUnits.Init();

            building.TryGetComponent<IWorkersCapacity>(out var capacity);
            capacity.Init(0, _autoLighterSettings.MaxUnitsCount);

            building.Init();

            slot.SetEnabled(false);

            return building;
        }

        private AutoCollectorBuilding BuildAutoCollector(BuildingSlot slot)
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<AutoCollectorBuilding>(
                    _autoCollectorSettings.AutoCollectorBuildingPrefab,
                    parentTransform.position, Quaternion.identity, parentTransform);

            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(0, _autoCollectorSettings.MaxGrade, _autoCollectorSettings.GradePrice);

            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(_autoCollectorSettings.MaxDurability, _autoCollectorSettings.MaxDurability);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init();

            building.TryGetComponent<IWorkersCapacity>(out var capacity);
            capacity.Init(0, _autoCollectorSettings.MaxUnitsCount);

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
            specUnits.Init();

            building.TryGetComponent<IWorkersCapacity>(out var capacity);
            capacity.Init(0, _factorySettings.MaxUnitsCount);

            building.Init();

            slot.SetEnabled(false);

            return building;
        }
    }
}