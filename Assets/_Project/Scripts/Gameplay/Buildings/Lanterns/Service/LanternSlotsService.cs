using System;
using System.Collections.Generic;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternSlotsService
    {
        [Inject] private LanternFactory _factory;
        [Inject] private LanternService _lanternService;
        [Inject] private readonly LanternSettings _settings;
        private readonly List<LanternSlot> _lanternSlots = new();
        private readonly List<LanternSlot> _initialLanternSlots = new();

        public void InitSlots(List<LanternSlotSpawnPoint> initBuildingsSpawnPoints,
            List<LanternSlotSpawnPoint> buildingsSpawnPoints)
        {
            foreach (var spawnPoint in initBuildingsSpawnPoints)
            {
                var slot = _factory.CreateSlotAtPosition(spawnPoint);
                _initialLanternSlots.Add(slot);
            }

            foreach (var spawnPoint in buildingsSpawnPoints)
            {
                var slot = _factory.CreateSlotAtPosition(spawnPoint);
                RegisterFreeSlot(slot);
            }
        }

        private void RegisterFreeSlot(LanternSlot slot)
        {
            _lanternSlots.Add(slot);
            SubscribeToSlot(slot);
        }

        private void SubscribeToSlot(LanternSlot slot)
        {
            slot.OnLanternBought += UpdateCosts;
            slot.OnDestroyed += UnsubscribeFromSlot;
        }

        private void UnsubscribeFromSlot(LanternSlot slot)
        {
            slot.OnLanternBought -= UpdateCosts;
            slot.OnDestroyed -= UnsubscribeFromSlot;
        }

        private void UpdateCosts()
        {
            _lanternSlots.ForEach(slot => slot.SetLanternCost(GetLanternCost()));
        }

        private int GetLanternCost()
        {
            var initCost = _settings.InitLanternCost;
            return (int) Math.Pow(initCost * _lanternService.GetLanternsCount(), _lanternService.GetLanternsCount());
        }

        public List<LanternSlot> GetInitialSlots()
        {
            return _initialLanternSlots;
        }
    }
}