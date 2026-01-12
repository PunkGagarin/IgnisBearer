using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(Button))]
    public class UiButtonEnableEffect : MonoBehaviour
    {
        [Inject] private readonly UiSettings _settings;

        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

        public void SetInteractable(bool isEnabled)
        {
            Button.interactable = isEnabled;
            CanvasGroup.alpha = isEnabled ? _settings.EnableNormalButtonValue : _settings.EnableDisabledButtonValue;
        }
    }
}