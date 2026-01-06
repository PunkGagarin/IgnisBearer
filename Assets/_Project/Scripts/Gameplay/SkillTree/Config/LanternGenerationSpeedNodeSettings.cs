using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "LanternLightGenerationSpeedNodeSettings", menuName = "Gameplay/LanternLightGenerationSpeedNodeSettings", order = 1)]
    public class LanternGenerationSpeedNodeSettings: SkillTreeNodeWithEffectSettings<LanternGenerationSpeedNodeEffectSettings>
    {
    }

    [Serializable]
    public class LanternGenerationSpeedNodeEffectSettings
    {
        [field: SerializeField]
        public float SpeedMultiplier { get; private set; }
    }
}