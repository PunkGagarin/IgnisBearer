using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{

    public class LightStorage : MonoBehaviour, ILightStorage
    {

        public int Amount { get; private set; }
        public int MaxAmount { get; private set; }

        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountIncreased = delegate { };
        public event Action OnStorageCleared = delegate { };

        public void Init(int maxStorage)
        {
            MaxAmount = maxStorage;
        }

        public void IncrementAmount(int amount)
        {
            Amount += amount;
            OnAmountIncreased.Invoke((amount, Amount, MaxAmount));
        }

        public void IncrementAmount()
        {
            Amount++;
            OnAmountIncreased.Invoke(((1, Amount, MaxAmount)));
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