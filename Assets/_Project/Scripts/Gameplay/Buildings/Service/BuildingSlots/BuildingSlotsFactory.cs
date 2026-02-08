using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Level;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.BuildingSlots
{
    public class BuildingSlotsFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly BuildingSlotsSettings _buildingSlotsSettings;
        [Inject] private readonly IBuildContainer _iBuildContainer;

        public BuildingSlot CreateSlotAtPosition(BuildingSlotsSpawnPoint buildingSlotsSpawnPoint)
        {
            var slot = _container.InstantiatePrefabForComponent<BuildingSlot>(_buildingSlotsSettings.Prefab,
                buildingSlotsSpawnPoint.transform.position, Quaternion.identity, _iBuildContainer.GetSlotsContainer());
            return slot;
        }
    }
}