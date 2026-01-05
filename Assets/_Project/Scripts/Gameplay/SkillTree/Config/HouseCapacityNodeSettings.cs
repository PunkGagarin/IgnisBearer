using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "HouseCapacityNodeSettings", menuName = "Gameplay/HouseCapacityNodeSettings", order = 1)]
    public class HouseCapacityNodeSettings : SkillNodeSettings
    {
        [field: SerializeField]
        public List<HouseCapacityEffectSettings> Effects { get; private set; }
    }
    
    [Serializable]
    public class HouseCapacityEffectSettings
    {
        [field: SerializeField]
        public int MaxCapacityIncrease { get; private set; }
    }
}