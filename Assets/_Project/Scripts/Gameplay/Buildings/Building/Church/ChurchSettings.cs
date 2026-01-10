using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "ChurchSettings", menuName = "Gameplay/Buildings/ChurchSettings", order = 0)]
    public class ChurchSettings : GradeSettings<ChurchBuilding, ChurchGradeData>
    {
        [field: SerializeField]
        public string BuildingNameKey { get; private set; }

        [field: SerializeField]
        public int StartLightAmount { get; private set; } = 10;

        public int MaxGrade => GradeData.Count;
    }
}