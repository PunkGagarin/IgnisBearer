using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui
{
    public class PausePopupController : MonoBehaviour
    {
        [Inject] private PausePopup _pausePopup;
        [field: SerializeField] private Button _pauseButton;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _pausePopup.OnClosed += ShowPauseButton;
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _pausePopup.OnClosed += ShowPauseButton;
        }

        private void ShowPauseButton()
        {
            _pauseButton.gameObject.SetActive(true);
        }

        private void OnPauseButtonClicked()
        {
            _pauseButton.gameObject.SetActive(false);
            _pausePopup.Show();
            
        }
    }
}