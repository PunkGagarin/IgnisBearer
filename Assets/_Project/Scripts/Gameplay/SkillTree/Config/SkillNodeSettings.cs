using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Gameplay.Ui.Tooltips;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "SkillTreeNodes", menuName = "Gameplay/SkillTreeNodes", order = 1)]
    public class SkillNodeSettings : ScriptableObject
    {
        [field: SerializeField]
        public SkillNodeType NodeType { get; private set; }

        [field: SerializeField] public int MaxLevel { get; private set; } = 1;

        [field: SerializeField] public MetaCurrencyType CurrencyType { get; private set; } = MetaCurrencyType.Dollars;

        [field: SerializeField] public List<int> Prices { get; private set; }

        [field: SerializeField] public Sprite Icon { get; private set; }
        
        [field: SerializeField] public Sprite MaxedIcon { get; private set; }
        
        [field: SerializeField] public TooltipUiData TooltipUiData { get; private set; }

        protected virtual void OnValidate()
        {
            if (Prices.Count != MaxLevel)
            {
                Debug.LogWarning($"Не совпадает количество цен и максимальный уровень!");
                for (int i = 0; i < MaxLevel - Prices.Count; i++)
                {
                    Prices.Add(0);
                }
            }

            if (NodeType == SkillNodeType.None)
            {
                Debug.LogError($"Не заполнен тип ноды!");
            }
        }
    }
}