using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class LevelInfo : MonoBehaviour
    {

        [field: SerializeField]
        public UnitSpawnPoint InitalUnitPosition { get; private set; }

        [field: SerializeField]
        public List<LanternSpawnPoint> InitalLanternPositions { get; private set; }
    }
}