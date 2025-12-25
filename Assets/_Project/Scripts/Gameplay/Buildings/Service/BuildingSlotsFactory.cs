using System;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingSlotsFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly BuildingSlotsSettings _buildingSlotsSettings;

        public BuildingSlot CreateSlotAtPosition(BuildingSlotsSpawnPoint buildingSlotsSpawnPoint,
            [CanBeNull] string slotId = null)
        {
            var slot = _container.InstantiatePrefabForComponent<BuildingSlot>(_buildingSlotsSettings.Prefab,
                buildingSlotsSpawnPoint.transform.position, Quaternion.identity, null);
            slot.Init(slotId ?? Guid.NewGuid().ToString());
            return slot;
        }
    }
}