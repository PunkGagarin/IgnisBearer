using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Data;
using _Project.Scripts.Gameplay.SkillTree;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class StartDataFactory
    {
        [Inject] private readonly BuildingSlotsSettings _settings;
        [Inject] private readonly SkillTreeFactory _factory;
        [Inject] private readonly BuildingSettings _buildingSettings;

        [Inject] private readonly PlayerDataService _playerDataService;

        public void CreateStartData()
        {
            CreatePrebuildBuildings();
            SetStartSlotsCount();
            CreateSkillTreeData();
        }

        private void CreatePrebuildBuildings()
        {
            _playerDataService.PlayerData.BuildingData.PrebuildBuildings = _buildingSettings.PrebuildBuildings;
        }

        private void SetStartSlotsCount()
        {
            _playerDataService.PlayerData.BuildingData.StartBuildingSlotCount = _settings.StartSlotsCount;
        }

        private void CreateSkillTreeData()
        {
            var treeData = _factory.Create();
            _playerDataService.PlayerData.SkillTreeData = treeData;
        }
    }
}