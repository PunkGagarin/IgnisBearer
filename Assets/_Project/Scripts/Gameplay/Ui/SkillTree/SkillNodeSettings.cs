using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui.SkillTree
{
    [Serializable]
    public class SkillNodeSettings
    {
        [field: SerializeField]
        public SkillNodeType NodeType { get; private set; }

        [field: SerializeField]
        public int MaxLevel { get; private set; }

    }
}