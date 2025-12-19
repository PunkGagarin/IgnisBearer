using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "HouseSettings", menuName = "Gameplay/Buildings/HouseSettings", order = 0)]
    public class HouseSettings: ScriptableObject
    {
        [field: SerializeField] public HouseBuilding Prefab { get; private set; }
        [field: SerializeField] public int MaxCountToBuild { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        [field: SerializeField] public float BuildPrice { get; private set; }
        [field: SerializeField] public int UnitCostMultiplier { get; private set; }
        [field: SerializeField] public int InitUnitCost { get; private set; }
        [field: SerializeField] public List<HouseGradeData> GradeData { get; private set; }
        public int MaxGrade => GradeData.Count;
    }
}