using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingSlotsService
    {
        [Inject] private BuildingFactory _buildingFactory;

        private List<BuildingSlot> _buildingSlots = new();
        private BuildingSlot _churchSlot;

        public void InitSlots(List<BuildingSlotsSpawnPoint> buildingsSpawnPoints,
            BuildingSlotsSpawnPoint churchBuildingSpawnPoint)
        {
            InitChurchSlot(churchBuildingSpawnPoint);
            foreach (var buildingSlotsSpawnPoint in buildingsSpawnPoints)
            {
                var slot = _buildingFactory.CreateSlotAtPosition(buildingSlotsSpawnPoint);
                _buildingSlots.Add(slot);
            }
        }

        private void InitChurchSlot(BuildingSlotsSpawnPoint churchBuildingSpawnPoint)
        {
            var church = _buildingFactory.CreateSlotAtPosition(churchBuildingSpawnPoint);
            _churchSlot = church;
        }
        
        public Vector3 GetChurchPosition()
        {
            return _churchSlot.transform.position;
        }

        public BuildingSlot GetChurchSlot()
        {
            return _churchSlot;
        }

        public BuildingSlot GetFirstSlot()
        {
            return _buildingSlots.First();
        }
    }
}