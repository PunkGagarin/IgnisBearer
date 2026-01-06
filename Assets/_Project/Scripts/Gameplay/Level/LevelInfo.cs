using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Level
{
    public class LevelInfo : MonoBehaviour
    {
        
        [field:SerializeField]
        public SpriteRenderer Background { get; private set; }

        [field: SerializeField]
        public UnitSpawnPoint InitalUnitPosition { get; private set; }

        [field: SerializeField]
        public List<LanternSlotSpawnPoint> InitalLanternSlotPositions { get; private set; }
        
        [field: SerializeField]
        public List<LanternSlotSpawnPoint> LanternSlotsPositions { get; private set; }
        
        [field: SerializeField]
        public List<BuildingSlotsSpawnPoint> InitalBuildingSlotsPositions { get; private set; }
        
        [field: SerializeField]
        public BuildingSlotsSpawnPoint ChurchBuildingSlotPosition { get; private set; }
        
        [field: SerializeField]
        public Transform SlotsContainer { get; private set; }
        
        [field: SerializeField]
        public Transform BuildingsContainer { get; private set; }
    }
}