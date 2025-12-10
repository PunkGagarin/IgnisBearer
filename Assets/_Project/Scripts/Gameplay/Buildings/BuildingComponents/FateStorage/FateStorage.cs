using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class FateStorage : MonoBehaviour, IFateStorage
    {
        public event Action OnAmountChanged = delegate { };
        public event Action OnStorageCleared = delegate { };
        public int Amount { get; private set; }
        public int MaxAmount { get; private set; }

        public void Init(int maxStorageCapacity)
        {
            MaxAmount = maxStorageCapacity;
        }

        public void IncrementAmount(int amount)
        {
            Amount += amount;
            OnAmountChanged.Invoke();
        }

        public void DecrementAmount(int amount)
        {
            Amount -= amount;
            OnAmountChanged.Invoke();
        }
        
        public void IncrementAmount()
        {
            Amount++;
            OnAmountChanged.Invoke();
        }

        public bool NotFull()
        {
            return Amount < MaxAmount;
        }

        public bool HasAny()
        {
            return Amount > 0;
        }

        private bool IsFull()
        {
            return !NotFull();
        }

        public int Harvest()
        {
            int amount = Amount;
            Debug.Log("Fate harvested " + Amount);

            Amount = 0;

            OnStorageCleared.Invoke();
            return amount;
        }
    }
}