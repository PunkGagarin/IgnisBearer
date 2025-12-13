using System;
using _Project.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class FateService
    {
        private IResourceStorage _fateStorage;

        event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountChanged = delegate { };

        public void Init(IResourceStorage storage)
        {
            _fateStorage = storage;
        }

        public bool HasEnough(int amount)
        {
            return _fateStorage.Amount >= amount;
        }

        public bool Spend(int amount)
        {
            if (!HasEnough(amount))
            {
                Debug.LogError($" Not enough fate to spend {amount}");
                return false;
            }

            _fateStorage.DecrementAmount(amount);
            return true;
        }
    }
}