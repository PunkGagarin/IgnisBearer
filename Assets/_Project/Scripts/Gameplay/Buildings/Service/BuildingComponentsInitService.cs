using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using _Project.Scripts.Gameplay.Ui.Buildings;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingComponentsInitService
    {
        [Inject] private readonly ChurchSettings _churchSettings;
        [Inject] private readonly BuildingSlotsSettings _buildingSlotsSettings;
        [Inject] private readonly HouseSettings _houseSettings;
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoHarvestSettings _autoHarvestSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private WorkerService _workerService;

        public int GetGradeData<T>(out T initGradeData, out T nextGradeData, List<T> listOfGrades, int initGrade)
            where T : IBaseGradeData
        {
            int index = initGrade - 1;

            if (index >= 0 && index < listOfGrades.Count)
                initGradeData = listOfGrades[index];
            else
                initGradeData = default;

            int nextIndex = index + 1;
            if (nextIndex >= 0 && nextIndex < listOfGrades.Count)
                nextGradeData = listOfGrades[nextIndex];
            else
                nextGradeData = default;

            return initGrade;
        }

        public Building InitBuildingComponents(Building building, int grade = 1)
        {
            return building switch
            {
                ChurchBuilding church => InitChurch(church, grade),
                AutoHarvestBuilding harvester => InitAutoHarvester(harvester, grade),
                AutoLighterBuilding lighter => InitAutoLighter(lighter, grade),
                HouseBuilding house => InitHouse(house, grade),
                FactoryBuilding factory => IniFactory(factory, grade),
                FateGeneratorBuilding fateGen => InitFateGenerator(fateGen, grade),
                _ => throw new ArgumentException("Неизвестный тип здания")
            };
        }

        private FateGeneratorBuilding InitFateGenerator(FateGeneratorBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _churchSettings.GradeData,
                grade);

            InitGrade(building, initGrade, _churchSettings.MaxGrade, nextGradeData.GradePrice);
            InitWorkers(building.gameObject, initGradeData.MaxUnitsCount);

            var fateResourceStorage = building.GetComponent<IResourceStorage>();
            fateResourceStorage.Init(int.MaxValue);

            var fateProducer = building.GetComponent<ResourceProducer>();
            fateProducer.Init(initGradeData.TimeToProduceFate);

            return building;
        }

        private ChurchBuilding InitChurch(ChurchBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _churchSettings.GradeData,
                grade);

            InitGrade(building, initGrade, _churchSettings.MaxGrade, nextGradeData.GradePrice);
            building.TryGetComponent<IResourceStorage>(out var lightStorage);
            lightStorage.Init(_churchSettings.StartLightAmount, initGradeData.MaxLightStorageCapacity);

            return building;
        }

        private HouseBuilding InitHouse(HouseBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _houseSettings.GradeData, grade);

            InitGrade(building, initGrade, _houseSettings.MaxGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);

            building.TryGetComponent<HouseBuyUnit>(out var buyUnit);
            buyUnit.Init(GetCurrentWorkersCount(), _houseSettings.InitUnitCost, _houseSettings.UnitCostMultiplier, initGradeData.MaxUnitsCount);

            return building;
        }

        private int GetCurrentWorkersCount()
        {
            return _workerService.WorkersCount();
        }

        private AutoLighterBuilding InitAutoLighter(AutoLighterBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _autoLighterSettings.GradeData,
                grade);

            InitGrade(building, initGrade, _autoLighterSettings.MaxGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building.gameObject, initGradeData.MaxUnitsCount);

            return building;
        }

        private AutoHarvestBuilding InitAutoHarvester(AutoHarvestBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _autoHarvestSettings.GradeData,
                grade);

            InitGrade(building, initGrade, _autoHarvestSettings.MaxGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building.gameObject, initGradeData.MaxUnitsCount);

            return building;
        }

        private FactoryBuilding IniFactory(FactoryBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _factorySettings.GradeData,
                grade);

            InitGrade(building, initGrade, _factorySettings.MaxGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building.gameObject, initGradeData.MaxUnitsCount);

            return building;
        }

        private static void InitWorkers(GameObject building, int maxUnitsCount)
        {
            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, maxUnitsCount);
        }

        private static void InitDurability(Building building, int maxDurability)
        {
            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(maxDurability, maxDurability);
        }

        private static void InitGrade(Building building, int initGrade, int maxGrade, int gradePrice)
        {
            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(initGrade, maxGrade, gradePrice);
        }
    }
}