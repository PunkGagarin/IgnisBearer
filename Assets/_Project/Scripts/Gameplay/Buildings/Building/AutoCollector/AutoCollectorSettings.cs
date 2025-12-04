using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.AutoCollector
{
    // [CreateAssetMenu(fileName = "AutoCollectorSettings", menuName = "Gameplay/Buildings/AutoCollectorSettings", order = 0)]
    public class AutoCollectorSettings: ScriptableObject
    {
        
        [field: SerializeField] public AutoCollectorBuilding AutoCollectorBuildingPrefab { get; private set; }
        [field: SerializeField] public double BuildPrice { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        
        [field: SerializeField] public int MaxUnitsCount { get; private set; }
        [field: SerializeField] public int GradePrice { get; private set; }
        [field: SerializeField] public int MaxDurability { get; private set; }
        [field: SerializeField] public int MaxGrade { get; private set; }


    }
}