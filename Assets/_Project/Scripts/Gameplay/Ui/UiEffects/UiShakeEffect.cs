using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(RectTransform))]
    public class UiShake : MonoBehaviour
    {
        [Inject] private readonly UiSettings _settings;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Shake()
        {
            _rectTransform.DOShakePosition(
                _settings.ShakeDuration,
                _settings.ShakeStrength,
                _settings.ShakeVibrato,
                _settings.ShakeRandomness
            );
        }
    }
}