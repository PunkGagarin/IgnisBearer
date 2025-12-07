using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{

    public class LightStorage : MonoBehaviour, ILightStorage
    {

        public int Amount { get; private set; }
        private int _maxAmount;

        public event Action OnAmountIncreased = delegate { };
        public event Action OnStorageCleared = delegate { };

        public void Init(int maxStorage)
        {
            _maxAmount = maxStorage;
        }

        public void IncrementAmount(int amount)
        {
            Amount += amount;
            OnAmountIncreased.Invoke();
        }

        public void IncrementAmount()
        {
            Amount++;
            OnAmountIncreased.Invoke();
        }

        public bool NotFull()
        {
            return Amount < _maxAmount;
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
            Debug.Log("Lantern harvested " + Amount);

            Amount = 0;

            OnStorageCleared.Invoke();
            return amount;
        }
    }
}