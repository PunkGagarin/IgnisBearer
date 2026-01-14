using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "AutoHarvestSettings", menuName = "Gameplay/Buildings/AutoHarvestSettings", order = 0)]
    public class AutoHarvestSettings : GradeSettings<AutoHarvestBuilding, AutoHarvestGradeData>, IBuildInfo
    {
        public BuildingType Type { get; private set; } = BuildingType.AutoHarvest;
        [field: SerializeField] public int MaxCountToBuild { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        [field: SerializeField] public float BuildPrice { get; private set; }
        public int MaxGrade => GradeData.Count;
        
    }
}