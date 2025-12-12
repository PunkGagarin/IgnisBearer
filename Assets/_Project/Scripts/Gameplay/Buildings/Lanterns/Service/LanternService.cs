using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Units;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternService
    {
        [Inject] private LanternFactory _factory;
        [Inject] private WorkerService _workers;

        private readonly List<Lantern> _lanterns = new();

        public event Action<Lantern> OnLanternFull = delegate { };
        public event Action<Lantern> OnLanternNeededToFire = delegate { };

        public void InitStartLanterns(List<LanternSpawnPoint> lanternPoints)
        {
            foreach (var lantern in _factory.CreateStartLanterns(lanternPoints))
                RegisterLantern(lantern);
        }

        public void RegisterLantern(Lantern lantern)
        {
            _lanterns.Add(lantern);
            SubscribeToLantern(lantern);
        }

        private void SubscribeToLantern(Lantern lantern)
        {
            lantern.OnDestroyed += UnsubscribeFromLantern;
            lantern.OnNeededToFire -= OnLanternNeededToFire;

            var clickDetector = lantern.GetComponent<LanternClickDetector>();
            clickDetector.OnClicked += OnLanternClicked;

            var lightStorage = lantern.GetComponent<LightStorage>();
            lightStorage.OnAmountFull += OnLanternFull;
        }

        private void UnsubscribeFromLantern(Lantern lantern)
        {
            lantern.OnDestroyed -= UnsubscribeFromLantern;
            lantern.OnNeededToFire -= OnLanternNeededToFire;

            var clickDetector = lantern.GetComponent<LanternClickDetector>();
            clickDetector.OnClicked -= OnLanternClicked;

            var lightStorage = lantern.GetComponent<LightStorage>();
            lightStorage.OnAmountFull -= OnLanternFull;
        }

        private void OnLanternFullHandle(Lantern obj)
        {
            OnLanternFull.Invoke(obj);
        }

        public void CreateAndRegisterLantern()
        {
            var lantern = _factory.CreateAndInstantiateLantern();
            RegisterLantern(lantern);
        }

        private void OnLanternClicked(Lantern lantern)
        {
            _workers.MoveFreeUnit(lantern);
        }

        public List<Lantern> GetUnfiredLanterns()
        {
            return _lanterns.Where(lantern => !lantern.IsFired()).ToList();
        }
    }
}