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
        private Vector2 _startPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
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


        public void ShakeError()
        {
            _rectTransform
                .DOShakeAnchorPos(
                    _settings.ErrorShakeDuration,
                    new Vector2(_settings.ErrorShakeStrength, 0f),
                    _settings.ErrorShakeVibrato
                )
                .OnComplete(() => _rectTransform.anchoredPosition = _startPosition);
        }
    }
}