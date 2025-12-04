using System.Collections.Generic;
using _Project.Scripts.Gameplay.Temporal;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Gameplay.Units.Manager;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternService
    {
        [Inject] private LanternFactory _factory;
        [Inject] private WorkerService _workers;

        private List<TemporalLantern> _lanterns = new();


        private void UnsubscribeFromLantern(TemporalLantern lantern)
        {
            lantern.OnDestroyed -= UnsubscribeFromLantern;
            var clickDetector = lantern.GetComponent<TempClickDetector>();
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

        public void RegisterLantern(TemporalLantern lantern)
        {
            _lanterns.Add(lantern);

            SubscribeToLantern(lantern);
        }

        private void SubscribeToLantern(TemporalLantern lantern)
        {
            lantern.OnDestroyed += UnsubscribeFromLantern;
            var clickDetector = lantern.GetComponent<TempClickDetector>();
            clickDetector.OnClicked += OnLanternClicked;
        }

        private void OnLanternClicked(TemporalLantern lantern)
        {
            _workers.MoveFreeUnit(lantern);
        }

    }
}