using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [CreateAssetMenu(fileName = "LanternSettings", menuName = "LanternSettings", order = 0)]
    public class LanternSettings : ScriptableObject
    {
        [field: SerializeField]
        public TemporalLantern Prefab { get; private set; }

        [field: SerializeField]
        public List<Transform> StartLanternPoints { get; private set; }
    }
}