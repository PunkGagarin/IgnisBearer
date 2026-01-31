using UnityEngine;

namespace _Project.Scripts.Shaders
{
    public class ChurchCrackController : MonoBehaviour
    {
        private static readonly int Radius = Shader.PropertyToID("_Radius");

        [Header("Material")] 
        [SerializeField] private Material material;

        [Header("Barrier")] 
        [Range(0, 100)]
        [SerializeField]
        private float _dangerThreshold = 5f;

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

        private float _barrierValue;
        private float _currentRadius;
        private float _targetRadius;
        private bool _isEnabled;

        private void Awake()
        {
            InitParams();
        }

        private void InitParams()
        {
            _currentRadius = 0f;
            _targetRadius = 0f;
            material.SetFloat(Radius, _currentRadius);
        }

        private void Update()
        {
            if (!_isEnabled)
                return;
            
            if (IsNotInDangerThreshold())
            {
                if (_targetRadius != 0)
                {
                    _targetRadius = 0f;
                    ApplyRuntimeParams();
                }
                return;
            }
            UpdateTargetRadius();
            SmoothRadius();
            ApplyRuntimeParams();
        }

        public void SetBarrierValue(float value)
        {
            _isEnabled = true;
            _barrierValue = Mathf.Clamp(value, 0f, 100f);
        }

        private bool IsBarrierDestroyed()
        {
            return _barrierValue <= 0f;
        }

        private bool IsNotInDangerThreshold()
        {
            return _barrierValue >= _dangerThreshold;
        }

        private void UpdateTargetRadius()
        {
            if (!_isEnabled)
                return;
            
            if (IsBarrierDestroyed())
            {
                _targetRadius = _maxRadius;
                return;
            }

            float t = 1f - _barrierValue / _dangerThreshold;
            float eased = EaseOutQuad(t);
            float rawRadius = eased * _maxRadius;

            _targetRadius = Mathf.Round(rawRadius / _step) * _step;
            _targetRadius = Mathf.Clamp(_targetRadius, 0f, _maxRadius);
        }


        private void SmoothRadius()
        {
            if (!_isEnabled)
                return;
            _currentRadius = Mathf.MoveTowards(
                _currentRadius,
                _targetRadius,
                _smoothSpeed * Time.deltaTime
            );
        }

        private void ApplyRuntimeParams()
        {
            if (!_isEnabled || !material)
                return;

            float radius = _currentRadius;

            float noise = (Mathf.PerlinNoise(Time.time * _noiseSpeed, 0f) - 0.5f) * _noiseStrength;
            radius += noise;
            if (_barrierValue <= _finalShakeThreshold && !IsBarrierDestroyed()) 
                radius += Mathf.Sin(Time.time * _finalShakeSpeed) * _finalShakeAmplitude;

            material.SetFloat(Radius, radius);
        }

        private float EaseOutQuad(float t)
        {
            return 1f - (1f - t) * (1f - t);
        }
    }

}