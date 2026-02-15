using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Gameplay.Ui.Tooltips;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "SkillTreeSettings", menuName = "Gameplay/SkillTreeSettings", order = 1)]
    public class SkillTreeSettings : ScriptableObject
    {
        [field: SerializeField]
        public List<SkillNodeSettings> SkillNodeSettings { get; set; }

        [field: SerializeField]
        public SkillNodeType InitNode { get; set; } = SkillNodeType.HouseCapacity;


        public MetaCurrencyType GetCurrencyTypeFor(SkillNodeType nodeType)
        {
            return GetSettingsFor(nodeType).CurrencyType;
        }

        public int GetPriceFor(SkillNodeType nodeType, int currentLevel)
        {
            var skillNodeSettings = GetSettingsFor(nodeType);

            if (currentLevel >= skillNodeSettings.Prices.Count)
            {
                Debug.LogError(
                    $" Запрашиваемый левел {currentLevel} больше чем максимальный {skillNodeSettings.Prices.Count}");
                return skillNodeSettings.Prices[^1];
            }

            return skillNodeSettings.Prices[currentLevel];
        }

        public int GetMaxLevelFor(SkillNodeType nodeType)
        {
            return GetSettingsFor(nodeType).MaxLevel;
        }

        public SkillNodeSettings GetSettingsFor(SkillNodeType nodeType)
        {
            var skillNodeSettings = SkillNodeSettings.FirstOrDefault(el => el.NodeType == nodeType);
            if (skillNodeSettings == null)
            {
                Debug.LogError($"SkillNodeSettings for {nodeType} not found");
                throw new Exception();
            }
            return skillNodeSettings;
        }

        public Sprite GetDefaultIconFor(SkillNodeType nodeType)
        {
            return GetSettingsFor(nodeType).Icon;
        }
        
        public Sprite GetMaxedIconFor(SkillNodeType nodeType)
        {
            return GetSettingsFor(nodeType).MaxedIcon;
        }

        public TooltipUiData GetTooltipUiDataFor(SkillNodeType nodeType)
        {
            return GetSettingsFor(nodeType).TooltipUiData;
        }
    }
}