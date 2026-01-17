using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    // [CreateAssetMenu(fileName = "LanternSettings", menuName = "LanternSettings", order = 0)]
    public class LanternSettings : ScriptableObject
    {
        [field: SerializeField]
        public LanternSlot SlotPrefab { get; private set; }

        [field: SerializeField]
        public Lantern Prefab { get; private set; }

        [field: SerializeField]
        public float FireUpTime { get; private set; } = 3f;

        [field: SerializeField]
        public int InitLanternCost { get; private set; } = 3;

        [field: SerializeField]
        public int LanternCostMultiplier { get; private set; } = 3;

        [field: SerializeField]
        public float HarvestTime { get; private set; } = 3f;

        [field: SerializeField]
        public float LightProduceTime { get; private set; } = 3f;

        [field: SerializeField]
        public int InitMaxStorage { get; private set; } = 1;

        [field: SerializeField]
        public int InitMaxResourceGeneratePerFireUp { get; private set; } = 6;

        [field: SerializeField]
        public float LightLifetime { get; private set; } = 15f;
    }
}