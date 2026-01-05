using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class HouseCapacityEffector : TreeNodeEffector
    {
        [Inject] private SkillTreeSettings _settings;
        
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.HouseCapacity;
        
        public override void ApplyEffect(int level)
        {
            HouseCapacityNodeSettings settings = _settings.GetSettingsFor(Type) as HouseCapacityNodeSettings;
            HouseCapacityEffectSettings houseCapacityEffectSettings = settings?.Effects[level-1];
            Debug.LogError($"House capacity will be increased after impl by: " +
                           $"{houseCapacityEffectSettings?.MaxCapacityIncrease}");
        }

        public override void RemoveEffect(int level)
        {
            HouseCapacityNodeSettings settings = _settings.GetSettingsFor(Type) as HouseCapacityNodeSettings;
            HouseCapacityEffectSettings houseCapacityEffectSettings = settings?.Effects[level-1];
            Debug.LogError($"House capacity should be removed before applying new effect by: " +
                           $"{houseCapacityEffectSettings?.MaxCapacityIncrease} ");
        }
    }
}