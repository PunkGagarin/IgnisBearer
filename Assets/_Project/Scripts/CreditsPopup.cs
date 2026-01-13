using _Project.Scripts.Audio.Domain;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts
{
    public class CreditsPopup : ContentUi
    {
        [SerializeField] private Button _closeButton;

        // [Inject] private AudioService _audioService;
        private void Awake() => _closeButton.onClick.AddListener(Close);

        private void OnDestroy() => _closeButton.onClick.RemoveListener(Close);

        private void Close()
        {
            // _audioService.PlaySound(Sounds.buttonClick);
            Hide();
        }
    }
}