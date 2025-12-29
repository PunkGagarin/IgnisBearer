using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui.SkillTree
{
    [CreateAssetMenu(fileName = "SkillTree", menuName = "Gameplay/SkillTree", order = 1)]
    public class SkillTreeSettings : ScriptableObject
    {
        [field: SerializeField]
        public List<SkillNodeSettings> SkillNodeSettings { get; set; }

        public MetaCurrencyType GetTypeFor(SkillNodeType nodeType)
        {
            throw new NotImplementedException();
        }

        public int GetPriceFor(SkillNodeType nodeType, int currentLevel)
        {
            throw new NotImplementedException();
        }
    }
}