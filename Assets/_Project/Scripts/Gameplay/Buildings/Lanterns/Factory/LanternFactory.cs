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
                var unit = _container.InstantiatePrefabForComponent<Lantern>(_settings.Prefab,
                    lanternPoint.transform.position, Quaternion.identity, null);
                var lightStorage = unit.GetComponent<LightStorage>();
                
                lightStorage.Init(_settings.InitMaxStorage);

                var lanternUi = unit.GetComponent<LanternUi>();
                lanternUi.Init();

                list.Add(unit);
            }
            return list;
        }
    }
}