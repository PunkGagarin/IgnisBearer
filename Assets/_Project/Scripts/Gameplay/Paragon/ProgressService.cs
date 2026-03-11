using System;
using _Project.Scripts.Gameplay.Ui;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Paragon {
    public class ProgressService : IInitializable, ITickable, IDisposable {
        private bool _isProgressing = false;
        private float _currentSessionTime = 0f;

        public Action OnProgressStarted = delegate { };

        [Inject] private readonly ParagonSettings _settings;

        // [Inject] private readonly ProgressTimeUi _ui;
        [Inject] private readonly MetaCurrencyService _currencyService;
        [Inject] private readonly GameEndService _gameEndService;

        private int _currentParagonGoal = 0;
        private int _timeToWin;
        private ParagonTimerSettings _currentGoal;

        public void Initialize() {
            _timeToWin = _settings.TimeToWin;
            GetCurrentGoalSettings();
            _gameEndService.OnGameEnded += TurnOffProgress;
        }

        public void Dispose() {
            _gameEndService.OnGameEnded -= TurnOffProgress;
        }

        private void GetCurrentGoalSettings() {
            _currentGoal = _settings.GetParagonTimeSettings(_currentParagonGoal);
        }

        public void TurnOnProgressing() {
            _isProgressing = true;
        }

        private void TurnOffProgress() {
            _isProgressing = false;
        }

        public bool IsProgressing() => _isProgressing;

        public void Tick() {
            if (!_isProgressing) return;
            _currentSessionTime += Time.deltaTime;
            // _ui.UpdateUi(_currentSessionTime, _timeToWin);

            if (_currentSessionTime >= _currentGoal.Second) {
                Debug.Log("Достигли точки парагона, выдаем валюту!");
                _currencyService.Add(_currentGoal.CurrencyType, _currentGoal.Amount);
                _currentParagonGoal++;
                GetCurrentGoalSettings();
            }
        }

    }
}