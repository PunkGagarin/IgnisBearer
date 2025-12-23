using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchFactory : BuildingFactory<ChurchBuilding, ChurchSettings, ChurchGradeData>
    {
        public override ChurchBuilding Create(BuildingSlot slot, int grade)
        {
            var building = InstantiateBuildingOnSlot(slot, _settings.Prefab);
            var gradeData = _settings.GetData(grade);
            var nextGradeData = _settings.GetNextData(grade);

            var nextGradePrice = nextGradeData?.GradePrice ?? 0;
            InitGradeComponent(building, grade, _settings.MaxGrade, nextGradePrice);

            building.TryGetComponent<IResourceStorage>(out var lightStorage);
            lightStorage.Init(_settings.StartLightAmount, gradeData.MaxLightStorageCapacity);

            slot.SetEnabled(false);
            return building;
        }
    }
}