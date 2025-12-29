using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoLighterFactory : BuildingFactory<AutoLighterBuilding, AutoLighterSettings, AutoLighterGradeData>
    {
        public override AutoLighterBuilding Create(BuildingSlot slot, int grade)
        {
            var building = InstantiateBuildingOnSlot(slot, _settings.Prefab);
            var gradeData = _settings.GetData(grade);
            var nextGradeData = _settings.GetNextData(grade);

            InitGradeComponent(building, grade, _settings.MaxGrade, nextGradeData.GradePrice);
            InitDurabilityComponent(building, gradeData.MaxDurability);
            InitWorkersComponent(building.gameObject, gradeData.MaxUnitsCount);

            slot.SetEnabled(false);
            return building;
        }
    }
}