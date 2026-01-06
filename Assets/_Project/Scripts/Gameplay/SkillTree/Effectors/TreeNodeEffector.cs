using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public abstract class TreeNodeEffector<Ts, Tes> : ITreeNodeEffector where Ts: SkillTreeNodeWithEffectSettings<Tes>
    {
        [Inject] private SkillTreeSettings _settings;
        
        public abstract SkillNodeType Type { get; protected set; }
        
        public virtual void ApplyEffect(int level)
        {
            var settings = _settings.GetSettingsFor(Type) as Ts;
            if (settings)
            {
                var effectSettings = settings.Effects[level - 1];
                AddEffect(effectSettings);
            }
            // Debug.LogError($"stat will be increased after impl by: " +
            //                $"{effectSettings?.MaxCapacityIncrease}");
        }

        protected abstract void AddEffect(Tes effectSettings);

        public virtual void RemoveEffect(int level)
        {
            var settings = _settings.GetSettingsFor(Type) as Ts;
            if (settings)
            {
                var effectSettings = settings.Effects[level - 1];
                RemoveEffect(effectSettings);
            }
            // Debug.LogError($"stat should be removed before applying new effect by: " +
            //                $"{effectSettings?.MaxCapacityIncrease} ");
        }

        protected abstract void RemoveEffect(Tes effectSettings);
    }
}