using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    // [CreateAssetMenu(fileName = "UnitSettings", menuName = "Gameplay/Units/UnitSettings", order = 0)]
    public class UnitSettings : ScriptableObject
    {
        [field: SerializeField]
        public Unit UnitPrefab { get; private set; }

        [field: SerializeField]
        public float DefaultMoveSpeed { get; private set; } = 5f;

        [field: SerializeField]
        public float DefaultFireUpSpeed { get; private set; } = 1f;

    }
}