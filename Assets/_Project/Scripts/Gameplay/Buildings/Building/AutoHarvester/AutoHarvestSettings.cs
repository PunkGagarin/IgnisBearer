using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "AutoHarvestSettings", menuName = "Gameplay/Buildings/AutoHarvestSettings", order = 0)]
    public class AutoHarvestSettings: ScriptableObject
    {
        
        [field: SerializeField] public AutoHarvestBuilding AutoHarvestBuildingPrefab { get; private set; }
        [field: SerializeField] public double BuildPrice { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        
        [field: SerializeField] public int MaxUnitsCount { get; private set; }
        [field: SerializeField] public int GradePrice { get; private set; }
        [field: SerializeField] public int MaxDurability { get; private set; }
        [field: SerializeField] public int MaxGrade { get; private set; }
        
    }
}