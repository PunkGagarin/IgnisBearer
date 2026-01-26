using System;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Data
{
    [Serializable]
    public class ChurchData
    {
        [field: JsonProperty]
        public int MaxGradeLevel { get; set; } = 1;
    }
}