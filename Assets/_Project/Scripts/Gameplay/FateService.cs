using System;
using _Project.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class FateService
    {
        private IResourceStorage _fateStorage;

        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountChanged = delegate { };

        public void Init(IResourceStorage storage)
        {
            _fateStorage = storage;
            _fateStorage.OnAmountIncreased += OnAmountIncreased;
            _fateStorage.OnDestroyed += Unsubscribe;
        }

        private void Unsubscribe()
        {
            _fateStorage.OnAmountIncreased -= OnAmountIncreased;
            _fateStorage.OnDestroyed -= Unsubscribe;
        }

        private void OnAmountIncreased((int amountIncreased, int newAmount, int maxAmount) obj) =>
            OnAmountChanged?.Invoke(obj);

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
            OnAmountChanged?.Invoke((amount, _fateStorage.Amount, _fateStorage.MaxAmount));
            return true;
        }
    }
}