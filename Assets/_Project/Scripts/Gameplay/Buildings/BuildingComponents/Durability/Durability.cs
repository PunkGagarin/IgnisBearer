using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.BuildingComponents.Durability
{
    public class Durability : MonoBehaviour, IDurability
    {
        public event Action<float> DurabilityChanged;
        public event Action OnDestroyed;

        public float Current { get; set; }
        public float Max { get; set; }

        public void UpdateDurability(float updatedDurability)
        {
            Current = updatedDurability;
            if (Current <= 0)
                OnDestroyed?.Invoke();
            DurabilityChanged?.Invoke(Current);
        }

        public void Init(float initValue, float maxValue)
        {
            Current = initValue;
            Max = maxValue;
        }
    }
}