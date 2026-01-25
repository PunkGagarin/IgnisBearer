using System;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(LightResource))]
    public class AutodestroyTimer : MonoBehaviour
    {
        public Action<LightResource> OnTimeUp { get; set; } = delegate { };

        [SerializeField]
        private float totalPulseDuration = 4f;

        [SerializeField]
        private float startPulseDuration = 0.6f;

        [SerializeField]
        private float endPulseDuration = 0.1f;

        [SerializeField]
        private float minAlpha = 0.25f;

        private SpriteRenderer _renderer;
        private Sequence _sequence;

        private bool _isTurned = true;
        private float _destroyTime;
        private float _currentTime;
        private bool _isPulsing;

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>(true);
        }

        public void Init(float destroyTime)
        {
            _destroyTime = destroyTime;
            _currentTime = 0f;
        }

        private void Update()
        {
            if (!_isTurned) return;
            
            _currentTime += Time.deltaTime;

            if (_currentTime >= _destroyTime - totalPulseDuration && !_isPulsing)
                StartPulseAnimation();

            if (_currentTime >= _destroyTime)
                OnTimeUp.Invoke(GetComponent<LightResource>());
        }

        private void StartPulseAnimation()
        {
            _isPulsing = true;
            _sequence = DOTween.Sequence().SetLink(gameObject);

            float elapsed = 0f;
            int steps = 12; // чем больше — тем плавнее ускорение

            for (int i = 0; i < steps; i++)
            {
                float t = i / (float)(steps - 1);
                float pulseDuration = Mathf.Lerp(startPulseDuration, endPulseDuration, t);

                _sequence
                    .Append(_renderer.DOFade(minAlpha, pulseDuration * 0.5f))
                    .Append(_renderer.DOFade(1f, pulseDuration * 0.5f));

                elapsed += pulseDuration;
            }

            _sequence
                .SetUpdate(false);
        }

        public void StopTimer()
        {
            _sequence?.Kill();
            _isTurned = false;
        }
    }
}