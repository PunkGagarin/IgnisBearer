using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.SkillTree;
using _Project.Scripts.Gameplay.Ui;
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
    public class CurrencyData
    {
        [field: JsonProperty]
        public List<MetaCurrencyType> Currencies { get; set; } = new();
    }

    [Serializable]
    public class BuildingData
    {
        [field: JsonProperty]
        public int StartBuildingSlotCount { get; set; }

        [field: JsonProperty]
        public int StartLanternSlotCount { get; set; }

        [field: JsonProperty]
        public List<BuildingType> PrebuildBuildings { get; set; } = new();

        [field: JsonProperty]
        public ChurchData ChurchData { get; set; }
    }

    [Serializable]
    public class ChurchData
    {
        [field: JsonProperty]
        public int MaxGradeLevel { get; set; } = 1;
    }
}