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

        public void InitSlots(List<LanternSlotSpawnPoint> initSpawnPoints,
            List<LanternSlotSpawnPoint> additionalSpawnPoints, int startSlotsCount)
        {
            foreach (var spawnPoint in initSpawnPoints)
            {
                var slot = _factory.CreateSlotAtPosition(spawnPoint);
                _initialLanternSlots.Add(slot);
            }

            int currentSlot = 1;
            foreach (var spawnPoint in additionalSpawnPoints)
            {
                if (currentSlot > startSlotsCount)
                    return;
                var slot = _factory.CreateSlotAtPosition(spawnPoint);
                RegisterFreeSlot(slot);
                currentSlot++;
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