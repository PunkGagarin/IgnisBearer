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
        [Inject] private LightResourceService _lightResourceService;

        private readonly List<Lantern> _lanterns = new();

        public event Action<Lantern> OnLanternNeededToFire = delegate { };
        public event Action<Lantern> OnLanternFired = delegate { };
        public event Action<Lantern> OnLanternCreated = delegate { };

        public void InitStartLanterns(List<LanternSlot> slots)
        {
            foreach (var lantern in _factory.CreateStartLanterns(slots))
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
            lantern.OnNeededToFire += OnLanternNeededToFireHandle;
            lantern.OnFired += OnLanternFiredHandle;

            var clickDetector = lantern.GetComponent<LanternClickDetector>();
            clickDetector.OnClicked += OnLanternClicked;
        }

        private void UnsubscribeFromLantern(Lantern lantern)
        {
            lantern.OnDestroyed -= UnsubscribeFromLantern;
            lantern.OnNeededToFire -= OnLanternNeededToFireHandle;
            lantern.OnFired -= OnLanternFiredHandle;

            var clickDetector = lantern.GetComponent<LanternClickDetector>();
            clickDetector.OnClicked -= OnLanternClicked;
        }

        private void OnLanternNeededToFireHandle(Lantern obj)
        {
            OnLanternNeededToFire.Invoke(obj);
        }

        private void OnLanternFiredHandle(Lantern lantern)
        {
            OnLanternFired.Invoke(lantern);
        }

        public void CreateAndRegisterLantern(LanternSlot slot)
        {
            var lantern = _factory.CreateLantern(slot);
            RegisterLantern(lantern);
            OnLanternCreated.Invoke(lantern);
        }

        private void OnLanternClicked(Lantern lantern)
        {
            if (_workers.MoveFreeUnitTo(lantern))
                lantern.GetComponent<LanternUi>().TurnOffIndicator();
            else
                lantern.GetComponent<FloatingMessage>().Play();
        }

        public List<Lantern> GetUnfiredLanterns()
        {
            return _lanterns.Where(lantern => !lantern.IsFired()).ToList();
        }

        public List<Lantern> GetUnharvestedLanterns()
        {
            return _lanterns.Where(lantern => lantern.GetComponent<IResourceStorage>().IsFull()).ToList();
        }

        public int GetLanternsCount()
        {
            return _lanterns.Count;
        }
    }
}