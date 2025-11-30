using UnityEngine;

namespace _Project.Scripts.Gameplay.House
{
    // [CreateAssetMenu(fileName = "HouseSettings", menuName = "Gameplay/Buildings/HouseSettings", order = 0)]
    public class HouseSettings: ScriptableObject
    {
        [field: SerializeField] public HouseBuilding HouseBuildingPrefab { get; private set; }
        
        [field: SerializeField] public int HouseGrade { get; private set; }
        [field: SerializeField] public int HouseCurrentUnitsCount { get; private set; }
        [field: SerializeField] public int HouseMaxUnitsCount { get; private set; }
        [field: SerializeField] public int HouseGradePrice { get; private set; }
        [field: SerializeField] public int HouseDurability { get; private set; }
    }
}