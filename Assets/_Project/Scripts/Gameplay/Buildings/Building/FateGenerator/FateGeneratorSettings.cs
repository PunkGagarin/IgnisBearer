using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    // [CreateAssetMenu(fileName = "FateGeneratorSettings", menuName = "Gameplay/Buildings/FateGeneratorSettings", order = 0)]
    public class FateGeneratorSettings : GradeSettings<FateGeneratorBuilding, FateGeneratorGradeData>, IBuildInfo
    {
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        [field: SerializeField] public int MaxCountToBuild { get; private set; }
        [field: SerializeField] public float BuildPrice { get; private set; } = 0;
        public int MaxGrade => GradeData.Count;
        public BuildingType Type { get; } = BuildingType.FateGenerator;
    }
}