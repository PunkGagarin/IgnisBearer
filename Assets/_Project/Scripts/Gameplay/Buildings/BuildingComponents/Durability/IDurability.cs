using System;

namespace _Project.Scripts.Gameplay.Buildings.BuildingComponents.Durability
{
    public interface IDurability
    {
        event Action<float> DurabilityChanged;
        public event Action OnDestroyed;
        float Current { get; set; }
        float Max { get; set; }
        void UpdateDurability(float updatedDurability);
        void Init(float initValue, float maxValue);
    }
}