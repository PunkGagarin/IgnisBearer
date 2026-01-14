using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoHarvesterFactory : BuildingFactory<AutoHarvestBuilding, AutoHarvestSettings, AutoHarvestGradeData>
    {
        public override AutoHarvestBuilding Create(BuildingSlot slot, int grade)
        {
            var building = InstantiateBuildingOnSlot(slot, _settings.Prefab);
            var gradeData = _settings.GetData(grade);
            var nextGradeData = _settings.GetNextData(grade);

            InitGradeComponent(building, grade, _settings.MaxGrade, nextGradeData.GradePrice);
            InitDurabilityComponent(building, gradeData.MaxDurability);
            InitWorkersComponent(building.gameObject, gradeData.MaxUnitsCount);
            InitGradeUpdateNotificationComponent(building.gameObject);
            
            slot.SetEnabled(false);
            return building;
        }
    }
}