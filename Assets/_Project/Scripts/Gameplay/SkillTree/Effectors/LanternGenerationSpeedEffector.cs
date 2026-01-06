using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class LanternGenerationSpeedEffector : TreeNodeEffector<LanternGenerationSpeedNodeSettings,
        LanternGenerationSpeedNodeEffectSettings>
    {
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.LanternGenerationSpeed;

        protected override void AddEffect(LanternGenerationSpeedNodeEffectSettings effectSettings)
        {
            Debug.LogError($"Lantern gen speed will be increased after impl by: " +
                           $"{effectSettings?.SpeedMultiplier}");
        }

        protected override void RemoveEffect(LanternGenerationSpeedNodeEffectSettings effectSettings)
        {
            Debug.LogError($"Lantern gen speed should be removed before applying new effect by: " +
                           $"{effectSettings?.SpeedMultiplier} ");
        }
    }
}