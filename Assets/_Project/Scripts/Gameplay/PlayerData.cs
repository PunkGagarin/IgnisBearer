using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsData;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay
{
    [Serializable]
    public class PlayerData
    {
        [field: JsonProperty] public Dictionary<int, int> SomeDic { get; set; } = new();

        [field: JsonProperty] public LevelContext LevelContext { get; set; } = new LevelContext();
        [field: JsonProperty] public BuildingsContext BuildingsContext { get; set; } = new();
    }

    [Serializable]
    public class BuildingsContext
    {
        [field: JsonProperty] public HouseContext HouseContext { get; set; } = new HouseContext();

        [field: JsonProperty] public ChurchContext ChurchContext { get; set; } = new ChurchContext();
        //other buildings & lanterns
    }

    [Serializable]
    public class HouseContext
    {
        [field: JsonProperty] public List<HouseBuildingData> BuildingsData { get; set; } = new();
    }

    [Serializable]
    public class ChurchContext
    {
        [field: JsonProperty] public ChurchBuildingData BuildingsData { get; set; } = new ChurchBuildingData();
    }

    [Serializable]
    public class LevelContext
    {
        [field: JsonProperty] public int CurrentLevel { get; set; }
    }
}