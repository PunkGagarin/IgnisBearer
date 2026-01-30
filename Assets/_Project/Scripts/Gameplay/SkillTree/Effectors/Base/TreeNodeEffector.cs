using _Project.Scripts.Gameplay.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public abstract class TreeNodeEffector<Ts, Tes> : ITreeNodeEffector
        where Ts : SkillTreeNodeWithEffectSettings<Tes> where Tes : class
    {
        [Inject] private SkillTreeSettings _settings;
        [Inject] private SkillTreeDataFacade _dataService;

        public abstract SkillNodeType Type { get; protected set; }

        public virtual void ApplyEffect(int level)
        {
            var effectSettings = EffectSettings(level);

            if (effectSettings != null)
                AddEffect(effectSettings);
        }

        protected Tes EffectSettings(int level)
        {
            Ts settings = _settings.GetSettingsFor(Type) as Ts;
            Tes effectSettings = settings?.Effects[level - 1];
            return effectSettings;
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
        }

        protected virtual void RemoveEffect(Tes effectSettings)
        {
            Debug.LogError(" was not implemented, shouldn't be called!!");
        }

        public bool IsPersist()
        {
            return _dataService.IsBought(Type);
        }

        protected int GetCurrentLevel()
        {
            return _dataService.GetLevelFor(Type);
        }
    }
}