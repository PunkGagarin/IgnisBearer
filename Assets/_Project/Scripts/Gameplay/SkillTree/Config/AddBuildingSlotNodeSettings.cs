using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "AddBuildingSlotNodeSettings", menuName = "Gameplay/AddBuildingSlotNodeSettings", order = 1)]
    public class AddBuildingSlotNodeSettings : SkillTreeNodeWithEffectSettings<AddBuildingSlotNodeEffectSettings>
    {
    }


    [Serializable]
    public class AddBuildingSlotNodeEffectSettings
    {
        [field: SerializeField]
        public int SlotCount { get; private set; }
    }
}