using System;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsData
{
    [Serializable]
    public class FateGeneratorBuildingData : BaseBuildingData, IStorageData
    {
        [field: JsonProperty] public int ResourceCount { get; set; }
    }
}