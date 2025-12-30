using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ui;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    [CreateAssetMenu(fileName = "SkillTreeNodes", menuName = "Gameplay/SkillTreeNodes", order = 1)]
    public class SkillNodeSettings : ScriptableObject
    {
        [field: SerializeField]
        public SkillNodeType NodeType { get; private set; }

        [field: SerializeField]
        public int MaxLevel { get; private set; } = 1;

        [field: SerializeField]
        public MetaCurrencyType CurrencyType { get; private set; } = MetaCurrencyType.Dollars;

        [field: SerializeField]
        public List<int> Prices { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        private void OnValidate()
        {
            if (Prices.Count != MaxLevel)
            {
                Debug.LogError($"Не совпадает количество цен и максимальный уровень!");
            }

            if (Prices.Count == 0)
            {
                Debug.LogError($"Не заполнены цены!");
            }

            if (NodeType == SkillNodeType.None)
            {
                Debug.LogError($"Не заполнен тип ноды!");
            }
        }
    }
}