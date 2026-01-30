using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Data;
using _Project.Scripts.Gameplay.SkillTree;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class StartDataFactory
    {
        [Inject] private readonly BuildingSlotsSettings _buildingSlotsSettings;
        [Inject] private readonly LanternSettings _lanternSettings;
        [Inject] private readonly SkillTreeFactory _factory;
        [Inject] private readonly BuildingSettings _buildingSettings;
        [Inject] private readonly ChurchSettings _churchSettings;

        [Inject] private readonly PlayerDataService _playerDataService;

        public void CreateStartData()
        {
            CreatePrebuildBuildings();
            SetStartSlotsCount();
            SetStartAvailableBuildings();
            SetChurchMaxLevel();
            CreateSkillTreeData();
        }

        private void SetStartAvailableBuildings()
        {
            foreach (var building in _buildingSettings.AvailableToBuildBuildings)
                _playerDataService.PlayerData.BuildingData.AvailableBuildings[building] = 1;
        }

        private void SetChurchMaxLevel()
        {
            _playerDataService.PlayerData.BuildingData.ChurchData.MaxGradeLevel = _churchSettings.MaxGrade;
        }

        private void CreatePrebuildBuildings()
        {
            _playerDataService.PlayerData.BuildingData.PrebuildBuildings = _buildingSettings.PrebuildBuildings;
        }

        private void SetStartSlotsCount()
        {
            _playerDataService.PlayerData.BuildingData.StartBuildingSlotCount = _buildingSlotsSettings.StartSlotsCount;
            _playerDataService.PlayerData.BuildingData.StartLanternSlotCount = _lanternSettings.StartSlotsCount;
        }

        private void CreateSkillTreeData()
        {
            var treeData = _factory.Create();
            _playerDataService.PlayerData.SkillTreeData = treeData;
        }
    }
}