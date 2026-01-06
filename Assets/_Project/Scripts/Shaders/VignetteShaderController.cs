using UnityEngine;

namespace _Project.Scripts.Shaders
{
    public class VignetteShaderController : MonoBehaviour
    {
        private static readonly int PulseSpeed = Shader.PropertyToID("_PulseSpeed");
        private static readonly int VignetteIntensity = Shader.PropertyToID("_VignetteIntensity");
        private static readonly int VignettePower = Shader.PropertyToID("_VignettePower");

        [Header("Fullscreen Share Material")] 
        [SerializeField]
        private Material _material;

        [Header("Vignette Settings")]
        [SerializeField]
        private float _vignettePower = 3f;

        [SerializeField] 
        private float _vignetteIntensity = 4f;

        [SerializeField]
        private float _pulseSpeed = 1f;

        [Header("Fade Settings")]
        [SerializeField] 
        private float _fadeSpeed = 2f;

        private float _currentVignetteIntensity;
        private float _targetVignetteIntensity;
        private bool _isEnabled;

        private void Awake()
        {
            ApplyAll();
            DisableEffect();
        }

        private void Update()
        {
            //todo for debug
            if (Input.GetKeyDown(KeyCode.E))
                EnableEffect();

            if (Input.GetKeyDown(KeyCode.Q))
                DisableEffect();

            if (!_isEnabled && IsNearTargetIntensity()) 
                return;
            _currentVignetteIntensity = Mathf.MoveTowards(
                _currentVignetteIntensity,
                _targetVignetteIntensity,
                Time.deltaTime * _fadeSpeed
            );

            if (_material != null)
                _material.SetFloat(VignetteIntensity, _currentVignetteIntensity);
        }

        private bool IsNearTargetIntensity()
        {
            return Mathf.Approximately(_currentVignetteIntensity, _targetVignetteIntensity);
        }

        private void OnValidate()
        {
            ApplyAll();
        }

        private void ApplyAll()
        {
            if (_material == null)
                return;

            _material.SetFloat(VignettePower, _vignettePower);
            _material.SetFloat(PulseSpeed, _pulseSpeed);
            _material.SetFloat(VignetteIntensity, _currentVignetteIntensity);
        }

        public void EnableEffect()
        {
            _isEnabled = true;
            _targetVignetteIntensity = _vignetteIntensity;
        }

        public void DisableEffect()
        {
            _isEnabled = false;
            _targetVignetteIntensity = 0f;
        }

        public bool IsEnabled() => _isEnabled;

        public void SetVignettePower(float value)
        {
            _vignettePower = value;
            _material.SetFloat(VignettePower, value);
        }

        public void SetVignetteIntensity(float value)
        {
            _vignetteIntensity = value;
            if (_isEnabled)
                _targetVignetteIntensity = value;
        }

        public void SetPulseSpeed(float value)
        {
            _pulseSpeed = value;
            _material.SetFloat(PulseSpeed, value);
        }
    }
}
