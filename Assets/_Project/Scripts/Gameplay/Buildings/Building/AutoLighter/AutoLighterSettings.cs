using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "AutoLighterSettings", menuName = "Gameplay/Buildings/AutoLighterSettings", order = 0)]
    public class AutoLighterSettings : GradeSettings<AutoLighterBuilding, AutoLighterGradeData>
    {
        [field: SerializeField] public int MaxCountToBuild { get; private set; }
        [field: SerializeField] public float BuildPrice { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        public int MaxGrade => GradeData.Count;
    }
}