using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    // [CreateAssetMenu(fileName = "UnitSettings", menuName = "Gameplay/Units/UnitSettings", order = 0)]
    public class UnitSettings : ScriptableObject
    {
        [field: SerializeField]
        public PeonUnit PeonUnitPrefab { get; private set; }

        [field: SerializeField]
        public float DefaultMoveSpeed { get; private set; }

    }
}