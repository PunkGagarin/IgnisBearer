using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Level;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class BuildingFactory<TBuilding, TSettings, TGradeDataSettings>
        where TSettings : GradeSettings<TBuilding, TGradeDataSettings> where TBuilding : Building
    {
        [Inject] protected readonly DiContainer _container;
        [Inject] protected readonly TSettings _settings;
        [Inject] protected readonly IBuildContainer _buildContainer;

        public abstract TBuilding Create(BuildingSlot slot, int grade);

        protected TBuilding InstantiateBuildingOnSlot(BuildingSlot slot, TBuilding prefab)
        {
            var building =
                _container.InstantiatePrefabForComponent<TBuilding>(prefab,
                    slot.transform.position, Quaternion.identity, _buildContainer.GetBuildingContainer());
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
            specUnits.Init(maxUnitsCount);
        }

        protected static void InitDurabilityComponent(Building building, int maxDurability)
        {
            building.TryGetComponent<IDurability>(out var durability);
            durability.Init(maxDurability, maxDurability);
        }
        
        protected void InitGradeUpdateNotificationComponent(GameObject buildingGameObject)
        {
            var gradeUpdateNotification = buildingGameObject.GetComponent<GradeUpdateNotification>();
            gradeUpdateNotification.Init();
        }
    }
}