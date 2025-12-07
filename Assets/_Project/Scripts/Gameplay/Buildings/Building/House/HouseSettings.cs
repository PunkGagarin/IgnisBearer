using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.House
{
    // [CreateAssetMenu(fileName = "HouseSettings", menuName = "Gameplay/Buildings/HouseSettings", order = 0)]
    public class HouseSettings: ScriptableObject
    {
        [field: SerializeField] public HouseBuilding HouseBuildingPrefab { get; private set; }
        
        [field: SerializeField] public int MaxUnitsCount { get; private set; }
        [field: SerializeField] public int GradePrice { get; private set; }
        [field: SerializeField] public int MaxDurability { get; private set; }
        [field: SerializeField] public int MaxGrade { get; private set; }
    }
}