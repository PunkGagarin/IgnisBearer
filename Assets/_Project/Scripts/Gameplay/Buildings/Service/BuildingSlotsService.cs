using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Data;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingSlotsService
    {
        [Inject] private readonly BuildingSlotsFactory _buildingSlotsFactory;
        [Inject] private readonly PlayerDataService _playerDataService;

        private List<BuildingSlot> _buildingSlots = new();
        private BuildingSlot _churchSlot;

        public void InitSlots(List<BuildingSlotsSpawnPoint> buildingsSpawnPoints,
            BuildingSlotsSpawnPoint churchBuildingSpawnPoint, int startSlotsCount)
        {
            InitChurchSlot(churchBuildingSpawnPoint);
            int currentSlot = 0;
            foreach (var buildingSlotsSpawnPoint in buildingsSpawnPoints)
            {
                //todo: madgine проверь что условие правильное (мб надо > а не >=)
                //и может быть что спавпоинтов меньше чем startSlotsCount
                if (currentSlot >= startSlotsCount)
                    return;

                var slot = _buildingSlotsFactory.CreateSlotAtPosition(buildingSlotsSpawnPoint);
                _buildingSlots.Add(slot);
                currentSlot++;
            }
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
            //todo: madgine get first FREE slot
            return _buildingSlots.First();
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