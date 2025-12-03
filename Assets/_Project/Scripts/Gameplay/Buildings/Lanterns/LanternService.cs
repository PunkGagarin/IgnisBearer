using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Temporal;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Gameplay.Units.Manager;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternService : IDisposable, IInitializable
    {
        [Inject] private LanternFactory _factory;
        [Inject] private WorkerService _workers;

        private List<TemporalLantern> _lanterns = new();


        public void Initialize()
        {
            InitStartLanterns();
        }

        public void Dispose()
        {
            foreach (var lantern in _lanterns)
                UnsubscribeFromLantern(lantern);
        }

        private void UnsubscribeFromLantern(TemporalLantern lantern)
        {
            var clickDetector = lantern.GetComponent<TempClickDetector>();
            clickDetector.OnClicked -= OnLanternClicked;
        }

        public void InitStartLanterns()
        {
            foreach (var lantern in _factory.CreateStartLanterns())
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
            var clickDetector = lantern.GetComponent<TempClickDetector>();
            SubscribeToLantern(clickDetector);
        }

        private void SubscribeToLantern(TempClickDetector clickDetector)
        {
            clickDetector.OnClicked += OnLanternClicked;
        }

        private void OnLanternClicked(TemporalLantern lantern)
        {
            _workers.MoveFreeUnit(lantern);
        }

    }
}