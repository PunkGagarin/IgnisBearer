using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface ILightStorage
    {
        event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountIncreased;
        event Action OnStorageCleared;
        int Amount { get; }
        int MaxAmount { get; }
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
        void IncrementAmount();
        bool NotFull();
        bool IsFull();
    }
}