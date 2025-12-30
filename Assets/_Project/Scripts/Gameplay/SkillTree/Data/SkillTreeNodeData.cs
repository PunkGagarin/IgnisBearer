using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.SkillTree
{
    [Serializable]
    public class SkillTreeNodeData
    {
        [field: JsonProperty]
        public SkillNodeState State { get; set; } = SkillNodeState.None;        
        
        [field: JsonProperty]
        public NodeBoughtState BoughtState { get; set; } = NodeBoughtState.None;

        [field: JsonProperty]
        public SkillNodeType Type { get; set; }  = SkillNodeType.None;

        [field: JsonProperty]
        public List<SkillNodeType> NextNodes { get; set; } = new();

        [field: JsonProperty]
        public SkillNodeType Parent { get; set; } = SkillNodeType.None;

        [field: JsonProperty]
        public int CurrentLevel { get; set; }

        [field: JsonProperty]
        public int MaxLevel { get; set; }
    }
}