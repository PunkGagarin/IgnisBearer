using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [CreateAssetMenu(fileName = "LanternSettings", menuName = "LanternSettings", order = 0)]
    public class LanternSettings : ScriptableObject
    {
        [field: SerializeField]
        public Lantern Prefab { get; private set; }

        [field: SerializeField]
        public float FireUpTime { get; private set; } = 3f;

    }
}