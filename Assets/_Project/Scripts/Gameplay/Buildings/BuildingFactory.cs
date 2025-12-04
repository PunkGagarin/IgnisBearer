using System;
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

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingFactory
    {
        [Inject] private readonly DiContainer _container;

        [Inject] private readonly ChurchSettings _churchSettings;
        [Inject] private readonly HouseSettings _houseSettings;
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoCollectorSettings _autoCollectorSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;

        public void BuildByType(BuildingType buildingType, BuildingSlot slot)
        {
            switch (buildingType)
            {
                case BuildingType.Church:
                    BuildChurch(slot);
                    break;
                case BuildingType.House:
                    BuildHouse(slot);
                    break;
                case BuildingType.Factory:
                    BuildFactory(slot);
                    break;
                case BuildingType.AutoCollector:
                    BuildAutoCollector(slot);
                    break;
                case BuildingType.AutoLighter:
                    BuildAutoLighter();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null);
            }
        }

        private ChurchBuilding BuildChurch(BuildingSlot slot)
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<ChurchBuilding>(_churchSettings.ChurchBuildingPrefab,
                    parentTransform.position, Quaternion.identity, parentTransform);

            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(0, _churchSettings.MaxGrade, _churchSettings.GradePrice);

            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(_churchSettings.MaxDurability, _churchSettings.MaxDurability);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init();

            building.TryGetComponent<IWorkersCapacity>(out var capacity);
            capacity.Init(0, _churchSettings.MaxUnitsCount);

            building.Init();

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
            grade.Init(0, _churchSettings.MaxGrade, _churchSettings.GradePrice);

            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(_churchSettings.MaxDurability, _churchSettings.MaxDurability);

            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init();

            building.TryGetComponent<IWorkersCapacity>(out var capacity);
            capacity.Init(0, _churchSettings.MaxUnitsCount);

            slot.SetEnabled(false);

            return building;
        }

        private void BuildAutoLighter()
        {
            throw new NotImplementedException();
        }

        private void BuildAutoCollector(BuildingSlot slot)
        {
            throw new NotImplementedException();
        }

        private void BuildFactory(BuildingSlot slot)
        {
            throw new NotImplementedException();
        }
    }
}