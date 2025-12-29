using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Ui.Buildings;
using _Project.Scripts.Gameplay.Units;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class HouseFactory : BuildingFactory<HouseBuilding, HouseSettings, HouseGradeData>
    {
        [Inject] private WorkerService _workerService;

        public override HouseBuilding Create(BuildingSlot slot, int grade)
        {
            var building = InstantiateBuildingOnSlot(slot, _settings.Prefab);
            var gradeData = _settings.GetData(grade);
            var nextGradeData = _settings.GetNextData(grade);

            InitGradeComponent(building, grade, _settings.MaxGrade, nextGradeData.GradePrice);
            InitDurabilityComponent(building, gradeData.MaxDurability);

            building.TryGetComponent<HouseBuyUnit>(out var buyUnit);
            buyUnit.Init(GetCurrentWorkersCount(), _settings.InitUnitCost, _settings.UnitCostMultiplier,
                gradeData.MaxUnitsCount);


            slot.SetEnabled(false);
            return building;
        }

        private int GetCurrentWorkersCount()
        {
            return _workerService.WorkersCount();
        }
    }
}