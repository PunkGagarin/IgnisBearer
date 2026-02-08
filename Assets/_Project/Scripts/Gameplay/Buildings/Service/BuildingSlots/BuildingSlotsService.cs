using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Data;
using _Project.Scripts.Gameplay.SkillTree.Effectors;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.BuildingSlots
{
    public class BuildingSlotsService
    {
        [Inject] private readonly BuildingSlotsFactory _buildingSlotsFactory;
        [Inject] private readonly PlayerDataService _playerDataService;
        [Inject] private readonly BuildingSlotsSettings _buildingSlotsSettings;
        [Inject] private readonly List<IBuildingSlotCountInfluencer> _influencers;
        
        private List<BuildingSlot> _buildingSlots = new();
        private BuildingSlot _churchSlot;

        public void InitSlots(
            List<BuildingSlotsSpawnPoint> buildingsSpawnPoints,
            BuildingSlotsSpawnPoint churchBuildingSpawnPoint
        )
        {
            InitChurchSlot(churchBuildingSpawnPoint);
            int currentSlot = 1;
            var slotsCountToDisplay = GetSlotsCountToDisplay();
            foreach (var buildingSlotsSpawnPoint in buildingsSpawnPoints)
            {
                if (currentSlot > slotsCountToDisplay)
                    return;

                var slot = _buildingSlotsFactory.CreateSlotAtPosition(buildingSlotsSpawnPoint);
                _buildingSlots.Add(slot);
                currentSlot++;
            }
        }

        private int GetSlotsCountToDisplay()
        {
            // todo: madgine
            var baseValue = _buildingSlotsSettings.StartSlotsCount;
            var mods = _influencers
                .Where(el => el.IsPersist())
                .Select(el => el.GetSlotsCount())
                .ToList();
            var countStat = new BuildingSlotStat(BuildingSlotStatType.Count, baseValue, mods);
            return (int)countStat.GetValue();
        }

        private void InitChurchSlot(BuildingSlotsSpawnPoint churchBuildingSpawnPoint)
        {
            var church = _buildingSlotsFactory.CreateSlotAtPosition(churchBuildingSpawnPoint);
            _churchSlot = church;
        }

        public BuildingSlot GetChurchSlot()
        {
            return _churchSlot;
        }

        public BuildingSlot GetFirstSlot()
        {
            return _buildingSlots.First(slot => !slot.IsTaken);
        }

        public void EnableButtonForSlots()
        {
            _churchSlot.SetButtonInteractable(true);
            foreach (var buildingSlot in _buildingSlots)
            {
                buildingSlot.SetButtonInteractable(true);
            }
        }
    }
}