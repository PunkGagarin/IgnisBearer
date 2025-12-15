using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "AutoHarvestSettings", menuName = "Gameplay/Buildings/AutoHarvestSettings", order = 0)]
    public class AutoHarvestSettings : ScriptableObject
    {
        [field: SerializeField] public AutoHarvestBuilding Prefab { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        [field: SerializeField] public float BuildPrice { get; private set; }
        [field: SerializeField] public List<AutoHarvestGradeData> GradeData { get; private set; }
        public int MaxGrade => GradeData.Count;
    }
}