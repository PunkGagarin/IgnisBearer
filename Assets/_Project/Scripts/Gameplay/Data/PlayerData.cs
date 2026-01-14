using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.SkillTree;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Data
{
    [Serializable]
    public class PlayerData
    {

        [field: JsonProperty]
        public LevelContext LevelContext { get; set; } = new();
        
        [field: JsonProperty]
        public SkillTreeData SkillTreeData { get; set; } = new();
    }

    [Serializable]
    public class LevelContext
    {
        [field: JsonProperty]
        public int CurrentLevel { get; set; }
    }
}