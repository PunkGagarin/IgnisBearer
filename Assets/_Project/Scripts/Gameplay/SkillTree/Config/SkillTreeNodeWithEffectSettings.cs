using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeNodeWithEffectSettings<T> : SkillNodeSettings
    {
        [field: SerializeField] public List<T> Effects { get; private set; }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (Effects.Count != MaxLevel)
            {
                Debug.LogWarning($"Не совпадает количество эффектов и максимальный уровень!");
                for (int i = 0; i < MaxLevel - Effects.Count; i++)
                {
                    Effects.Add(default);
                }
            }
        }
    }
}