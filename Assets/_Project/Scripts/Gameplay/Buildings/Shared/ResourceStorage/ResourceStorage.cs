using System;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ResourceStorage : MonoBehaviour, IResourceStorage
    {
        public int Amount { get; private set; }
        public int MaxAmount { get; private set; }
        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountIncreased = delegate { };
        public event Action<Lantern> OnAmountFull = delegate { };
        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountDecreased = delegate { };
        public event Action OnReachZero = delegate { };
        public event Action OnStorageCleared = delegate { };
        public event Action OnStartHarvest = delegate { };
        public event Action OnDestroyed = delegate { };

        public void Init(int maxStorage)
        {
            MaxAmount = maxStorage;
        }

        public void Init(int startAmount, int maxStorage)
        {
            Amount = startAmount;
            MaxAmount = maxStorage;
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public void DecrementAmount(int amount)
        {
            Amount -= amount;
            OnAmountDecreased.Invoke((amount, Amount, MaxAmount));
            if (Amount <= 0)
            {
                Amount = 0;
                OnReachZero?.Invoke();
            }
        }

        public void IncrementAmount()
        {
            int amountIncreased = 1;
            IncrementAmount(amountIncreased);
        }

        public void IncrementAmount(int amount)
        {
            Amount += amount;

            if (Amount > MaxAmount)
                Amount = MaxAmount;

            OnAmountIncreased.Invoke((amount, Amount, MaxAmount));

            if (Amount == MaxAmount)
                OnAmountFull.Invoke(GetComponent<Lantern>());
        }

        public bool NotFull()
        {
            return Amount < MaxAmount;
        }

        public bool HasAny()
        {
            return Amount > 0;
        }

        public bool IsFull()
        {
            return !NotFull();
        }

        public void StartCollecting()
        {
            OnStartHarvest.Invoke();
        }

        public int Collect()
        {
            int amount = Amount;
            Debug.Log("Lantern harvested " + Amount);

            Amount = 0;

            OnStorageCleared.Invoke();
            return amount;
        }
    }
}