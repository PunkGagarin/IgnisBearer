using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingSlotsService
    {
        [Inject] private BuildingSlotsFactory _buildingSlotsFactory;

        private List<BuildingSlot> _buildingSlots = new();
        private BuildingSlot _churchSlot;

        public void InitSlots(List<BuildingSlotsSpawnPoint> buildingsSpawnPoints,
            BuildingSlotsSpawnPoint churchBuildingSpawnPoint)
        {
            InitChurchSlot(churchBuildingSpawnPoint);
            foreach (var buildingSlotsSpawnPoint in buildingsSpawnPoints)
            {
                var slot = _buildingSlotsFactory.CreateSlotAtPosition(buildingSlotsSpawnPoint);
                _buildingSlots.Add(slot);
            }
        }

        private void InitChurchSlot(BuildingSlotsSpawnPoint churchBuildingSpawnPoint)
        {
            var church = _buildingSlotsFactory.CreateSlotAtPosition(churchBuildingSpawnPoint);
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