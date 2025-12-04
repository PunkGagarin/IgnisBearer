using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Factory
{
    // [CreateAssetMenu(fileName = "FactorySettings", menuName = "Gameplay/Buildings/FactorySettings", order = 0)]
    public class FactorySettings : ScriptableObject
    {
        [field: SerializeField] public FactoryBuilding FactoryBuildingPrefab { get; private set; }
        [field: SerializeField] public double BuildPrice { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        
        [field: SerializeField] public int MaxUnitsCount { get; private set; }
        [field: SerializeField] public int GradePrice { get; private set; }
        [field: SerializeField] public int MaxDurability { get; private set; }
        [field: SerializeField] public int MaxGrade { get; private set; }

    }
}