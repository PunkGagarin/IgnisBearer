using System.Collections.Generic;
using _Project.Scripts.Gameplay.Temporal;
using _Project.Scripts.Gameplay.Units.Manager;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternService
    {
        [Inject] private LanternFactory _factory;
        [Inject] private WorkerService _workers;

        private List<Lantern> _lanterns = new();


        private void UnsubscribeFromLantern(Lantern lantern)
        {
            lantern.OnDestroyed -= UnsubscribeFromLantern;
            var clickDetector = lantern.GetComponent<LanternClickDetector>();
            clickDetector.OnClicked -= OnLanternClicked;
        }

        public void InitStartLanterns(List<LanternSpawnPoint> lanternPoints)
        {
            foreach (var lantern in _factory.CreateStartLanterns(lanternPoints))
            {
                RegisterLantern(lantern);
                _lanterns.Add(lantern);
            }
        }

        public void CreateAndRegisterLantern()
        {
            var lantern = _factory.CreateAndInstantiateLantern();
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
            var clickDetector = lantern.GetComponent<LanternClickDetector>();
            clickDetector.OnClicked += OnLanternClicked;
        }

        private void OnLanternClicked(Lantern lantern)
        {
            _workers.MoveFreeUnit(lantern);
        }

    }
}