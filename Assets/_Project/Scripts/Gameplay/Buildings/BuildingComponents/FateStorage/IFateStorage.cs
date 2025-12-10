using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IFateStorage
    {
        public event Action OnAmountChanged;
        public event Action OnStorageCleared;
        int Amount { get; }
        int MaxAmount { get; }
        int Harvest();
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
        void DecrementAmount(int amount);
        void IncrementAmount();
        bool NotFull();
        bool HasAny();
    }
}