using System;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsData
{
    [Serializable]
    public class BuildingData
    {
        public BuildingData(string slotId, int currentGrade)
        {
            SlotId = slotId;
            CurrentGrade = currentGrade;
        }

        protected BuildingData()
        {
        }
        [field: JsonProperty] public string SlotId { get; set; }
        [field: JsonProperty] public int CurrentGrade { get; set; }
    }
}