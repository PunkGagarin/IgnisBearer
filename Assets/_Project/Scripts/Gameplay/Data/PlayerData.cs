using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.SkillTree;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Data
{
    [Serializable]
    public class PlayerData
    {
        [field: JsonProperty]
        public BuildingData BuildingData { get; set; } = new();

        [field: JsonProperty]
        public SkillTreeData SkillTreeData { get; set; } = new();

        [field: JsonProperty]
        public CurrencyData CurrencyData { get; set; } = new();
    }

    [Serializable]
    public class BuildingData
    {
        [field: JsonProperty]
        public List<BuildingType> PrebuildBuildings { get; set; } = new();
        
        [field: JsonProperty]
        public Dictionary<BuildingType, int> AvailableBuildings { get; set; } = new();

        [field: JsonProperty]
        public ChurchData ChurchData { get; set; } = new();
    }
}