using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Data;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchFactory : BuildingFactory<ChurchBuilding, ChurchSettings, ChurchGradeData>
    {
        [Inject] private readonly PlayerDataService _dataService;
        
        public override ChurchBuilding Create(BuildingSlot slot, int grade)
        {
            var building = InstantiateBuildingOnSlot(slot, _settings.Prefab);
            var gradeData = _settings.GetData(grade);
            var nextGradeData = _settings.GetNextData(grade);

            var nextGradePrice = nextGradeData?.GradePrice ?? 0;
            var maxLevel = _dataService.PlayerData.BuildingData.ChurchData.MaxGradeLevel;
            InitGradeComponent(building, grade, maxLevel, nextGradePrice);

            building.TryGetComponent<IResourceStorage>(out var lightStorage);
            lightStorage.Init(_settings.StartLightAmount, gradeData.MaxLightStorageCapacity);
            
            building.TryGetComponent<ChurchQueue>(out var churchQueue);
            churchQueue.Init(gradeData.QueueCapacity);

            InitGradeUpdateNotificationComponent(building.gameObject);
            
            slot.SetEnabled(false);
            return building;
        }
    }
}