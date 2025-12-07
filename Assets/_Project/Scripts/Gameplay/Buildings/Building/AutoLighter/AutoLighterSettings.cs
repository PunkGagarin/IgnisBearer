using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "AutoLighterSettings", menuName = "Gameplay/Buildings/AutoLighterSettings", order = 0)]
    public class AutoLighterSettings : ScriptableObject
    {
        [field: SerializeField] public AutoLighterBuilding AutoLighterBuildingPrefab { get; private set; }
        [field: SerializeField] public double BuildPrice { get; private set; }
        [field: SerializeField] public string BuildingNameKey { get; private set; }
        
        [field: SerializeField] public int MaxUnitsCount { get; private set; }
        [field: SerializeField] public int GradePrice { get; private set; }
        [field: SerializeField] public int MaxGrade { get; private set; }
        [field: SerializeField] public int MaxDurability { get; private set; }

    }
}