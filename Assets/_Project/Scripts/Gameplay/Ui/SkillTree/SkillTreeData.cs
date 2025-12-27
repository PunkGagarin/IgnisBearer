using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Ui.SkillTree
{
    [Serializable]
    public class SkillTreeData
    {
        [field: JsonProperty]
        public  bool IsUnlocked { get; set; }

        [field: JsonProperty]
        public List<SkillTreeNodeData> Nodes { get; set; } = new();
    }

}