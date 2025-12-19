using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class FateService : IInitializable, IDisposable
    {
        private IResourceStorage _fateStorage;
        private bool _isInited;

        [Inject] private readonly BuildingsService _buildingService;

        public event Action<(int amountIncreased, int newAmount, int maxAmount)> OnAmountChanged = delegate { };

        public void Initialize()
        {
            _buildingService.OnFateGeneratorBuilt += Init;
        }

        public void Dispose()
        {
            _buildingService.OnFateGeneratorBuilt -= Init;
        }

        public void Init(FateGeneratorBuilding fateGenerator)
        {
            var storage = fateGenerator.GetComponent<IResourceStorage>();
            _fateStorage = storage;
            _fateStorage.OnAmountIncreased += OnAmountIncreased;
            _fateStorage.OnDestroyed += Unsubscribe;
            _isInited = true;
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
            if (amount == 0)
                return true;

            if (!_isInited)
                return false;

            return _fateStorage.Amount >= amount;
        }

        public bool Spend(int amount)
        {
            if (!_isInited || !HasEnough(amount))
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