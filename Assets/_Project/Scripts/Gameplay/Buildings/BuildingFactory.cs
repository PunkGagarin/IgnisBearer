using _Project.Scripts.Gameplay.BuildingComponents.Durability;
using _Project.Scripts.Gameplay.BuildingComponents.Grade;
using _Project.Scripts.Gameplay.BuildingComponents.Workers;
using _Project.Scripts.Gameplay.BuildingComponents.WorkersCapacity;
using _Project.Scripts.Gameplay.BuildingsSlots;
using _Project.Scripts.Gameplay.Church;
using _Project.Scripts.Gameplay.House;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class BuildingFactory
    {
        [Inject] private readonly DiContainer _container;

        [Inject] private readonly ChurchSettings _churchSettings;
        [Inject] private readonly HouseSettings _houseSettings;

        public ChurchBuilding BuildChurch(BuildingSlot slot)
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

        public HouseBuilding BuildHouse(BuildingSlot slot)
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
    }
}