using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class LevelInfo : MonoBehaviour
    {
        
        [field:SerializeField]
        public SpriteRenderer Background { get; private set; }

        [field: SerializeField]
        public UnitSpawnPoint InitalUnitPosition { get; private set; }

        [field: SerializeField]
        public List<LanternSpawnPoint> InitalLanternPositions { get; private set; }
        
        [field: SerializeField]
        public List<BuildingSlotsSpawnPoint> InitalBuildingSlotsPositions { get; private set; }
        
        [field: SerializeField]
        public BuildingSlotsSpawnPoint ChurchBuildingSlotPosition { get; private set; }
    }
}