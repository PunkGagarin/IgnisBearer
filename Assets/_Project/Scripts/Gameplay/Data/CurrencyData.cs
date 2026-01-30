using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ui;
using Newtonsoft.Json;

namespace _Project.Scripts.Gameplay.Data
{
    [Serializable]
    public class CurrencyData
    {
        [field: JsonProperty]
        public Dictionary<MetaCurrencyType, int> Currencies { get; set; } = new();
    }
}