using Jam.Scripts.Audio.Domain;
using Project.Scripts.Audio;
using Project.Scripts.Infrastructure.GameStates;
using Project.Scripts.Infrastructure.GameStates.States;
using Project.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _startGame;

        [SerializeField]
        private Button _settings;

        [SerializeField]
        private Button _credits;

        [Inject] private SceneLoader _sceneLoader;
        [Inject] private GameStateMachine _stateMachine;
        [Inject] private AudioService _audio;


        private void Awake()
        {
            _startGame.onClick.AddListener(StartGame);
            _settings.onClick.AddListener(OpenSettings);
            _credits.onClick.AddListener(OpenCredits);
        }

        private void OnDestroy()
        {
            _startGame.onClick.RemoveListener(StartGame);
            _settings.onClick.RemoveListener(OpenSettings);
            _credits.onClick.RemoveListener(OpenCredits);
        }

        private void StartGame()
        {
            _audio.PlaySound(Sounds.buttonClick);
            _stateMachine.Enter<LoadGameplayState>();
        }

        private void OpenSettings()
        {
        }

        private void OpenCredits()
        {
        }
    }
}