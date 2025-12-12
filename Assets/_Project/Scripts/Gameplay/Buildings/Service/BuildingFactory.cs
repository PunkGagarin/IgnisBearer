using System;
using System.Collections.Generic;
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
            var building = InstantiateBuildingOnSlot<ChurchBuilding>(slot, _churchSettings.Prefab);
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _churchSettings.GradeData);
            
            InitGrade(building, initGrade, nextGradeData.GradePrice);
            InitWorkers(building, initGradeData.MaxUnitsCount);

            building.TryGetComponent<IResourceStorage>(out var lightStorage);
            lightStorage.Init(initGradeData.MaxLightStorageCapacity);

            building.TryGetComponent<IFateProducer>(out var fateGenerator);
            fateGenerator.Init(initGradeData.TimeToProduceFate, initGradeData.AmountToProduceFateAtTime);

            building.TryGetComponent<IResourceStorage>(out var fateStorage);
            fateStorage.Init(initGradeData.MaxFateStorageCapacity);

            building.TryGetComponent<ILightConsumer>(out var lightConsumer);
            lightConsumer.Init(initGradeData.LightConsumeTime, initGradeData.LightConsumeAmount);

            slot.SetEnabled(false);

            return building;
        }


        private HouseBuilding BuildHouse(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<HouseBuilding>(slot, _houseSettings.Prefab);
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _houseSettings.GradeData);

            InitGrade(building, initGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building, initGradeData.MaxUnitsCount);

            slot.SetEnabled(false);

            return building;
        }

        private AutoLighterBuilding BuildAutoLighter(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<AutoLighterBuilding>(slot, _autoLighterSettings.Prefab);
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _autoLighterSettings.GradeData);

            InitGrade(building, initGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building, initGradeData.MaxUnitsCount);
            
            slot.SetEnabled(false);

            return building;
        }

        private AutoHarvestBuilding BuildAutoHarvester(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<AutoHarvestBuilding>(slot, _autoHarvestSettings.Prefab);
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _autoHarvestSettings.GradeData);

            InitGrade(building, initGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building, initGradeData.MaxUnitsCount);
            
            slot.SetEnabled(false);

            return building;
        }


        private FactoryBuilding BuildFactory(BuildingSlot slot)
        {
            var building = InstantiateBuildingOnSlot<FactoryBuilding>(slot, _factorySettings.Prefab);
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _factorySettings.GradeData);
            
            InitGrade(building, initGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building, initGradeData.MaxUnitsCount);
            
            slot.SetEnabled(false);

            return building;
        }

        private T InstantiateBuildingOnSlot<T>(BuildingSlot slot, Building prefab) where T : Building
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<T>(prefab,
                    parentTransform.position, Quaternion.identity, parentTransform);
            return building;
        }

        private int GetGradeData<T>(out T initGradeData, out T nextGradeData, List<T> listOfGrades) where T : IBaseGradeData
        {
            var initGrade = 0;
            initGradeData = listOfGrades[initGrade];
            nextGradeData = listOfGrades[1];
            return initGrade;
        }

        private static void InitWorkers(Building building, int maxUnitsCount)
        {
            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, maxUnitsCount);
        }

        private static void InitDurability(Building building, int maxDurability)
        {
            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(maxDurability, maxDurability);
        }

        private static void InitGrade(Building building, int initGrade, int gradePrice)
        {
            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(initGrade, gradePrice);
        }
    }
}