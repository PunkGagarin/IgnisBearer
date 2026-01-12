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
        private Vector2 _originalAnchoredPos;


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalAnchoredPos = _rectTransform.anchoredPosition;
        }

        public void Shake()
        {
            _rectTransform.DOShakeAnchorPos(_settings.ShakeDuration, _settings.ShakeStrength, _settings.ShakeVibrato, _settings.ShakeRandomness)
                .OnComplete(() => _rectTransform.anchoredPosition = _originalAnchoredPos);
        }
    }
}