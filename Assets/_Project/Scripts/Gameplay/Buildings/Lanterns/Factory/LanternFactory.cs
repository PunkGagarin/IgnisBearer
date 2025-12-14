using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LanternSettings _settings;

        public Lantern CreateAndInstantiateLantern()
        {
            return _settings.Prefab;
        }

        public List<Lantern> CreateStartLanterns(List<LanternSpawnPoint> lanternPoints)
        {
            var list = new List<Lantern>();

            foreach (var lanternPoint in lanternPoints)
            {
                var lantern = _container.InstantiatePrefabForComponent<Lantern>(_settings.Prefab,
                    lanternPoint.transform.position, Quaternion.identity, null);
                lantern.Init( _settings.InitMaxHarvestCount);
                
                var lightStorage = lantern.GetComponent<ResourceStorage>();
                lightStorage.Init(_settings.InitMaxStorage);

                var lanternUi = lantern.GetComponent<LanternUi>();
                lanternUi.Init();
                
                var lightProducer = lantern.GetComponent<ResourceProducer>();
                lightProducer.Init(_settings.LightProduceTime);

                list.Add(lantern);
            }
            return list;
        }
    }
}