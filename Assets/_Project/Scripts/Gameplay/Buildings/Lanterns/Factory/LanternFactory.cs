using System.Collections.Generic;
using _Project.Scripts.Gameplay.Level;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LanternSettings _settings;
        [Inject] private readonly IBuildContainer _iBuildContainer;

        public LanternSlot CreateSlotAtPosition(LanternSlotSpawnPoint slotSpawnPoint)
        {
            var slot = _container.InstantiatePrefabForComponent<LanternSlot>(_settings.SlotPrefab,
                slotSpawnPoint.transform.position, Quaternion.identity, _iBuildContainer.GetSlotsContainer());
            slot.Init(_settings.InitLanternCost);
            return slot;
        }

        public Lantern CreateLantern(LanternSlot slot)
        {
            return CreateLanternAt(slot);
        }

        public List<Lantern> CreateStartLanterns(List<LanternSlot> slots)
        {
            var list = new List<Lantern>();

            foreach (var slot in slots)
            {
                var lantern = CreateLanternAt(slot);
                slot.Hide();
                list.Add(lantern);
            }
            return list;
        }

        private Lantern CreateLanternAt(LanternSlot slot)
        {
            var lantern = _container.InstantiatePrefabForComponent<Lantern>(_settings.Prefab,
                slot.transform.position, Quaternion.identity, _iBuildContainer.GetBuildingContainer());
            lantern.Init( _settings.InitMaxResourceGeneratePerFireUp);

            var lanternUi = lantern.GetComponent<LanternUi>();
            lanternUi.Init();
                
            var lightProducer = lantern.GetComponent<ResourceProducer>();
            lightProducer.Init(_settings.LightProduceTime);

            return lantern;
        }
    }
}