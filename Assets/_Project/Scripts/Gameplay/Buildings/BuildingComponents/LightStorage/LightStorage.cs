using System;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{

    public class LightStorage : MonoBehaviour, ILightStorage
    {

        public int Amount { get; private set; }
        public int MaxAmount { get; private set; }
        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountIncreased = delegate { };
        public event Action<Lantern> OnAmountFull = delegate { };
        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountDecreased = delegate { };
        public event Action OnStorageCleared = delegate { };
        public event Action OnStartHarvest = delegate { };

        public void Init(int maxStorage)
        {
            MaxAmount = maxStorage;
        }

        public void DecrementAmount(int amount)
        {
            Amount -= amount;
            OnAmountDecreased.Invoke((amount, Amount, MaxAmount));
        }

        public void IncrementAmount()
        {
            int amountIncreased = 1;
            IncrementAmount(amountIncreased);
        }

        public void IncrementAmount(int amount)
        {
            Amount += amount;
            OnAmountIncreased.Invoke((amount, Amount, MaxAmount));

            if (Amount > MaxAmount)
                Amount = MaxAmount;

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

        public void StartHarvest()
        {
            OnStartHarvest.Invoke();
        }

        public int Harvest()
        {
            int amount = Amount;
            Debug.Log("Lantern harvested " + Amount);

            Amount = 0;

            OnStorageCleared.Invoke();
            return amount;
        }
    }
}