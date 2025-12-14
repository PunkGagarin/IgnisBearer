using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    // [CreateAssetMenu(fileName = "LanternSettings", menuName = "LanternSettings", order = 0)]
    public class LanternSettings : ScriptableObject
    {
        [field: SerializeField]
        public Lantern Prefab { get; private set; }

        [field: SerializeField]
        public float FireUpTime { get; private set; } = 3f;

        [field: SerializeField]
        public float HarvestTime { get; private set; } = 3f;
        
        [field: SerializeField]
        public float LightProduceTime { get; private set; } = 3f;

        [field: SerializeField]
        public int InitMaxStorage { get; private set; } = 1;
        
        [field: SerializeField]
        public int InitMaxHarvestCount { get; private set; } = 3;

    }
}