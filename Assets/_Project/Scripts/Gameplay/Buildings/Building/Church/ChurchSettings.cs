using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "ChurchSettings", menuName = "Gameplay/Buildings/ChurchSettings", order = 0)]
    public class ChurchSettings: ScriptableObject
    {
        [field: SerializeField] public ChurchBuilding ChurchBuildingPrefab { get; private set; }
        
        [field: SerializeField] public int MaxGrade { get; private set; }
        [field: SerializeField] public int MaxUnitsCount { get; private set; }
        [field: SerializeField] public int MaxFateStorageCapacity { get; private set; }
        [field: SerializeField] public int MaxLightStorageCapacity { get; private set; }
        [field: SerializeField] public int GradePrice { get; private set; }
        
    }
}