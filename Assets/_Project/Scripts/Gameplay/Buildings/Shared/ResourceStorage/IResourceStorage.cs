using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IResourceStorage
    {
        event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountIncreased;
        event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountDecreased;
        event Action OnStorageCleared;
        event Action OnDestroyed;
        event Action OnStartHarvest;
        int Amount { get; }
        int MaxAmount { get; }
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
        void IncrementAmount();
        void DecrementAmount(int amount);
        bool NotFull();
        bool HasAny();
        bool IsFull();
        event Action OnReachZero;
    }
}