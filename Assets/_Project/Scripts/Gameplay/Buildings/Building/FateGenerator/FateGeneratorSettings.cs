using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    // [CreateAssetMenu(fileName = "FateGeneratorSettings", menuName = "Gameplay/Buildings/FateGeneratorSettings", order = 0)]
    public class FateGeneratorSettings : ScriptableObject
    {
        [field: SerializeField]
        public FateGeneratorBuilding Prefab { get; private set; }

        [field: SerializeField]
        public string BuildingNameKey { get; private set; }
        
        [field: SerializeField]
        public int MaxCountToBuild { get; private set; }

        [field: SerializeField]
        public float BuildPrice { get; private set; } = 0;

        [field: SerializeField]
        public List<FateGeneratorGradeData> GradeData { get; private set; }

        public int MaxGrade => GradeData.Count;
    }
}