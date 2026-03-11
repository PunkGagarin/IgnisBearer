using System;
using _Project.Scripts.Audio.Domain;
using _Project.Scripts.Audio.View;
using _Project.Scripts.Infrastructure.SceneManagement;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui
{
    public class PausePopup : ContentUi
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _playButton;

        [Inject] private SceneLoader _sceneLoader;
        [Inject] private SettingsView _settingsView;
        [Inject] private AudioService _audioService;

        public Action OnClosed = delegate { };

        private void Awake()
        {
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            _audioService.PlaySound(Sounds.buttonClick.ToString());
            Hide();
            OnClosed.Invoke();
        }

        private void OnSettingsButtonClick()
        {
            _audioService.PlaySound(Sounds.buttonClick.ToString());
            _settingsView.Show();
        }

        private async void OnMainMenuButtonClick()
        {
            OnClosed.Invoke();
            _audioService.PlaySound(Sounds.buttonClick.ToString());
            await _sceneLoader.LoadScene(SceneEnum.MainMenu);
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
        }
    }
}