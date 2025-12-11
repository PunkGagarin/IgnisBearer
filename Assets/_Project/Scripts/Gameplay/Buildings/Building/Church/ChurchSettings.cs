using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "ChurchSettings", menuName = "Gameplay/Buildings/ChurchSettings", order = 0)]
    public class ChurchSettings : ScriptableObject
    {
        [field: SerializeField]
        public ChurchBuilding Prefab { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        [field: SerializeField] public List<ChurchGradeData> GradeData { get; private set; }

    }
}