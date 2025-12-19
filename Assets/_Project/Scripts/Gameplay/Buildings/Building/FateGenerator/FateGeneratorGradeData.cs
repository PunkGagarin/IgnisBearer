using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    [Serializable]
    public class FateGeneratorGradeData : IBaseGradeData
    {
        [field: SerializeField]
        public int GradePrice { get; set; }

        [field: SerializeField]
        public int MaxUnitsCount { get; set; }

        [field: SerializeField]
        public int MaxDurability { get; set; }
    }
}