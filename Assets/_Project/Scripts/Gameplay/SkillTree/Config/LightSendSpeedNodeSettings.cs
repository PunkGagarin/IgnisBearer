using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "AddAutoHarvestNodeSettings", menuName = "Gameplay/AddAutoHarvestNodeSettings", order = 1)]
    public class LightSendSpeedNodeSettings : SkillTreeNodeWithEffectSettings<LightSendSpeedNodeEffectSettings>
    {
    }


    [Serializable]
    public class LightSendSpeedNodeEffectSettings
    {
        [field: SerializeField]
        public ModifierType ModifierType { get; set; }
        
        [field: SerializeField]
        public float Value { get; set; }
    }
}