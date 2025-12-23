using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class BuildingFactory<TBuilding, TSettings, TGradeDataSettings>
        where TSettings : GradeSettings<TBuilding, TGradeDataSettings> where TBuilding : Building
    {
        [Inject] protected readonly DiContainer _container;
        [Inject] protected readonly TSettings _settings;

        public abstract TBuilding Create(BuildingSlot slot, int grade);

        protected TBuilding InstantiateBuildingOnSlot(BuildingSlot slot, TBuilding prefab)
        {
            var parentTransform = slot.transform;
            var building =
                _container.InstantiatePrefabForComponent<TBuilding>(prefab,
                    parentTransform.position, Quaternion.identity, parentTransform);
            return building;
        }
        
        protected static void InitGradeComponent(Building building, int initGrade, int maxGrade, int gradePrice)
        {
            building.TryGetComponent<IGrade>(out var grade);
            grade.Init(initGrade, maxGrade, gradePrice);
        }
        protected static void InitWorkersComponent(GameObject building, int maxUnitsCount)
        {
            building.TryGetComponent<IWorkers>(out var specUnits);
            specUnits.Init(0, maxUnitsCount);
        }

        protected static void InitDurabilityComponent(Building building, int maxDurability)
        {
            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(maxDurability, maxDurability);
        }
    }
}