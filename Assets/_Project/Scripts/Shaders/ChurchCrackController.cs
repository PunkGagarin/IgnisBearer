using UnityEngine;

namespace _Project.Scripts.Shaders
{
    public class ChurchCrackController : BarrierVfxController
    {
        private static readonly int Radius = Shader.PropertyToID("_Radius");
        
        [Range(0f, 0.2f)] 
        [SerializeField] private float _maxRadius = 0.2f;

        [Header("Step")]
        [SerializeField] private float _step = 0.05f;

        [Header("Smooth")]
        [SerializeField] private float _smoothSpeed = 0.4f;

        [Header("Noise")]
        [SerializeField] private float _noiseStrength = 0.01f;
        [SerializeField] private float _noiseSpeed = 6f;

        [Header("Final Tension")] 
        [SerializeField]
        private float _finalShakeThreshold = 1f;
        [SerializeField] private float _finalShakeAmplitude = 0.015f;
        [SerializeField] private float _finalShakeSpeed = 40f;
        [SerializeField] private CrackCameraShake _cameraShake;

        private float _currentRadius;
        private float _targetRadius;
        private float _lastStepRadius;

        protected override void InitParams()
        {
            _currentRadius = 0f;
            _targetRadius = 0f;
            _lastStepRadius = 0f;
            _material.SetFloat(Radius, _currentRadius);
        }

        private void Update()
        {
            if (!IsEnabled())
                return;

            if (IsNotInDangerThreshold())
            {
                if (_targetRadius != 0f)
                {
                    _targetRadius = 0f;
                    _lastStepRadius = 0f;
                    ApplyRuntimeParams();
                }

                return;
            }

            UpdateTargetRadius();
            SmoothRadius();
            ApplyRuntimeParams();
        }

        private void UpdateTargetRadius()
        {
            if (!IsEnabled())
                return;
            
            if (IsBarrierDestroyed())
            {
                _targetRadius = _maxRadius;
                SetDisabled();
                return;
            }

            float t = 1f - _barrierValue / _appearThreshold;
            float eased = EaseOutQuad(t);
            float rawRadius = eased * _maxRadius;

            _targetRadius = Mathf.Round(rawRadius / _step) * _step;
            _targetRadius = Mathf.Clamp(_targetRadius, 0f, _maxRadius);

            if (_targetRadius > _lastStepRadius)
            {
                _cameraShake?.Shake();
                _lastStepRadius = _targetRadius;
            }
        }

        private void SmoothRadius()
        {
            if (!IsEnabled())
                return;
            _currentRadius = Mathf.MoveTowards(
                _currentRadius,
                _targetRadius,
                _smoothSpeed * Time.deltaTime
            );
        }

        protected override void ApplyRuntimeParams()
        {
            if (!IsEnabled() || !_material)
                return;

            float radius = _currentRadius;

            float noise = (Mathf.PerlinNoise(Time.time * _noiseSpeed, 0f) - 0.5f) * _noiseStrength;
            radius += noise;

            if (_barrierValue <= _finalShakeThreshold && !IsBarrierDestroyed())
            {
                radius += Mathf.Sin(Time.time * _finalShakeSpeed) * _finalShakeAmplitude;
            }

            _material.SetFloat(Radius, radius);
        }

        private float EaseOutQuad(float t)
        {
            return 1f - (1f - t) * (1f - t);
        }
    }
}