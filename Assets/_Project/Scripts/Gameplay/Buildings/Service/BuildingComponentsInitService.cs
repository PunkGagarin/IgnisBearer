using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Ui.Buildings;
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

        public void InitGradeForChurch(ChurchBuilding church)
        {
            int initGradeValue = 1;
            var initGrade = GetGradeData(out _, out var nextGradeData, _churchSettings.GradeData,
                initGradeValue);

            InitGrade(church, initGrade, _churchSettings.MaxGrade, nextGradeData.GradePrice);
        }

        public Building InitBuildingComponents(Building building, int grade = 1)
        {
            return building switch
            {
                ChurchBuilding church => BuildChurch(church, grade),
                AutoHarvestBuilding harvester => BuildAutoHarvester(harvester, grade),
                AutoLighterBuilding lighter => BuildAutoLighter(lighter, grade),
                HouseBuilding house => BuildHouse(house, grade),
                FactoryBuilding factory => BuildFactory(factory, grade),
                _ => throw new ArgumentException("Неизвестный тип здания")
            };
        }

        private ChurchBuilding BuildChurch(ChurchBuilding building, int grade)
        {
            GetGradeData(out var initGradeData, out _, _churchSettings.GradeData,
                grade);

            building.TryGetComponent<IResourceStorage>(out var lightStorage);
            lightStorage.Init(_churchSettings.StartLightAmount, initGradeData.MaxLightStorageCapacity);

            FateGeneratorInit(building, initGradeData);

            return building;
        }

        private void FateGeneratorInit(ChurchBuilding building, ChurchGradeData initGradeData)
        {
            var fateGenerator = building.FateGenerator;
            InitWorkers(fateGenerator, initGradeData.MaxUnitsCount);

            var fateResourceStorage = fateGenerator.GetComponent<IResourceStorage>();
            fateResourceStorage.Init(int.MaxValue);

            var fateProducer = fateGenerator.GetComponent<ResourceProducer>();
            fateProducer.Init(initGradeData.TimeToProduceFate);
        }


        private HouseBuilding BuildHouse(HouseBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _houseSettings.GradeData, grade);

            InitGrade(building, initGrade, _houseSettings.MaxGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);

            building.TryGetComponent<HouseBuyUnit>(out var buyUnit);
            buyUnit.Init(initGradeData.UnitCost, initGradeData.MaxUnitsCount);

            return building;
        }

        private AutoLighterBuilding BuildAutoLighter(AutoLighterBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _autoLighterSettings.GradeData,
                grade);

            InitGrade(building, initGrade, _autoLighterSettings.MaxGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building.gameObject, initGradeData.MaxUnitsCount);

            return building;
        }

        private AutoHarvestBuilding BuildAutoHarvester(AutoHarvestBuilding building, int grade)
        {
            var initGrade = GetGradeData(out var initGradeData, out var nextGradeData, _autoHarvestSettings.GradeData,
                grade);

            InitGrade(building, initGrade, _autoHarvestSettings.MaxGrade, nextGradeData.GradePrice);
            InitDurability(building, initGradeData.MaxDurability);
            InitWorkers(building.gameObject, initGradeData.MaxUnitsCount);

            return building;
        }

        private FactoryBuilding BuildFactory(FactoryBuilding building, int grade)
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