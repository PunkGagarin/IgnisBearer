using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "FactorySettings", menuName = "Gameplay/Buildings/FactorySettings", order = 0)]
    public class FactorySettings : GradeSettings<FactoryBuilding, FactoryGradeData>
    {
        [field: SerializeField] public int MaxCountToBuild { get; private set; }
        [field: SerializeField] public float BuildPrice { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        public int MaxGrade => GradeData.Count;
    }
}