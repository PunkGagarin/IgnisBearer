using System;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsData
{
    [Serializable]
    public class ChurchBuildingData : BuildingData, IWorkersData, IStorageData
    {
        [field: JsonProperty] public int WorkersCount { get; set; }
        [field: JsonProperty] public int ResourceCount { get; set; }
    }
}