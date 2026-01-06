using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class HouseCapacityEffector : TreeNodeEffector<HouseCapacityNodeSettings, HouseCapacityEffectSettings>
    {
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.HouseCapacity;

        protected override void AddEffect(HouseCapacityEffectSettings effectSettings)
        {
            Debug.LogError($"House capacity will be increased after impl by: " +
                           $"{effectSettings?.MaxCapacityIncrease}");
        }

        protected override void RemoveEffect(HouseCapacityEffectSettings effectSettings)
        {
            Debug.LogError($"House capacity should be removed before applying new effect by: " +
                           $"{effectSettings?.MaxCapacityIncrease} ");
        }
    }
}