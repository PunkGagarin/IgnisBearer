using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    // [CreateAssetMenu(fileName = "UnitSettings", menuName = "Gameplay/Units/UnitSettings", order = 0)]
    public class UnitSettings : ScriptableObject
    {
        [field: SerializeField]
        public Unit UnitPrefab { get; private set; }

        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 5f;

        [field: SerializeField]
        public float FireUpMultiplier { get; private set; } = 1f;

        [field: SerializeField]
        public float SendLightToChurchMultiplier { get; private set; } = 4f;

        [field: SerializeField]
        public float IdleBeforeMoveMinTime { get; private set; } = 1f;

        [field: SerializeField]
        public float IdleBeforeMoveMaxTime { get; private set; } = 5f;
    }
}