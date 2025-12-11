using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "FactorySettings", menuName = "Gameplay/Buildings/FactorySettings", order = 0)]
    public class FactorySettings : ScriptableObject
    {
        [field: SerializeField] public FactoryBuilding Prefab { get; private set; }
        [field: SerializeField] public double BuildPrice { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        [field: SerializeField] public List<FactoryGradeData> GradeData { get; private set; }

    }
}