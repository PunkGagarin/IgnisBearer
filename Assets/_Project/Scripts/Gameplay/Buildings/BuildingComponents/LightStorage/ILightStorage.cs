using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface ILightStorage
    {
        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountIncreased;
        int Amount { get; }
        int MaxAmount { get; }
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
        void IncrementAmount();
        bool NotFull();
    }
}