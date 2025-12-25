using System;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsData
{
    [Serializable]
    public class BaseBuildingData : BuildingData, IWorkersData, IDurabilityData
    {
        [field: JsonProperty] public int WorkersCount { get; set; }
        [field: JsonProperty] public int DurabilityLevel { get; set; }
    }
}