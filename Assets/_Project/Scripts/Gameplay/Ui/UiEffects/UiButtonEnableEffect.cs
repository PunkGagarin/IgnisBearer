using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(Button))]
    public class UiButtonEnableEffect : MonoBehaviour
    {
        [Inject] private readonly UiSettings _settings;
        
        private Button _button;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        public void SetInteractable(bool isEnabled)
        {
            _button.interactable = isEnabled;
            _text.color = isEnabled ? _settings.EnableNormalButtonColor : _settings.EnableDisabledButtonColor;
        }
    }
}