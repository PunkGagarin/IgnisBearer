using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay
{
    [Serializable]
    public class PlayerData
    {

        [field: JsonProperty]
        public Dictionary<int, int> SomeDic { get; set; } = new();

        [field: JsonProperty]
        public LevelContext LevelContext { get; set; } = new LevelContext();
    }

    [Serializable]
    public class LevelContext
    {
        [field: JsonProperty]
        public int CurrentLevel { get; set; }
    }
}