using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LanternSettings _settings;

        public TemporalLantern CreateAndInstantiateLantern()
        {
            return _settings.Prefab;
        }

        public List<TemporalLantern> CreateStartLanterns(List<LanternSpawnPoint> lanternPoints)
        {
            var list = new List<TemporalLantern>();

            foreach (var lanternPoint in lanternPoints)
            {
                var unit = _container.InstantiatePrefabForComponent<TemporalLantern>(_settings.Prefab,
                    lanternPoint.transform.position, Quaternion.identity, null);

                list.Add(unit);
            }
            return list;
        }
    }
}