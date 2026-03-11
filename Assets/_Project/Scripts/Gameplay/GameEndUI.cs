using _Project.Scripts.Infrastructure.SceneManagement;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts
{
    public class GameEndUI : ContentUi
    {
        [Inject] private readonly SceneLoader _sceneLoader;
        
        [field:SerializeField]
        public Button RestartButton { get; private set; }

        private void Awake()
        {
             RestartButton.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            RestartButton.onClick.RemoveListener(Restart);
        }

        private async void Restart()
        {
            await _sceneLoader.ReloadScene();
        }
    }
}