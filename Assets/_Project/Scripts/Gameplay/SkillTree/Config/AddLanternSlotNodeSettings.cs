using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    [CreateAssetMenu(fileName = "AddLanternSlotNodeSettings", menuName = "Gameplay/AddLanternSlotNodeSettings", order = 1)]
    public class AddLanternSlotNodeSettings : SkillTreeNodeWithEffectSettings<AddLanternSlotNodeEffectSettings>
    {
    }


    [Serializable]
    public class AddLanternSlotNodeEffectSettings
    {
        [field: SerializeField]
        public int SlotCount { get; private set; }
    }
}