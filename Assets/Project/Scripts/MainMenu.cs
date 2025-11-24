using Project.Scripts.SceneManagement;
using Project.Scripts.Utils.Coroutine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _credits;

        [Inject] private SceneLoader _sceneLoader;
        [Inject] private CoroutineHelper _coroutineHelper;
        
        
        
        private void Awake()
        {
            _startGame.onClick.AddListener(StartGame);
            _settings.onClick.AddListener(OpenSettings);
            _credits.onClick.AddListener(OpenCredits);
        }
        
        private async void StartGame()
        {
            _sceneLoader.LoadScene(SceneEnum.Gameplay).Forget();
        }
        
        private void OpenSettings()
        {
            
        }
        
        private void OpenCredits()
        {
            
        }
    }
}
