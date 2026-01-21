using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    public class UiPulseShot : MonoBehaviour
    {
        [field: SerializeField] private float _pulseScale = 1.2f;
        [field: SerializeField] private float _duration = 0.2f;

        private Vector2 _startScale;

        protected virtual void Awake()
        {
            _startScale = transform.localScale;
        }

        public void PlayPulse()
        {
            transform.DOKill();

            transform
                .DOScale(_startScale * _pulseScale, _duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transform.DOScale(_startScale, _duration)
                        .SetEase(Ease.InQuad);
                });
        }
    }
}