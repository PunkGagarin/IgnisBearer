using UnityEngine;

namespace _Project.Scripts.Shaders
{
    public class VignetteShaderController : BarrierVfxController
    {
        private static readonly int PulseSpeedID = Shader.PropertyToID("_PulseSpeed");
        private static readonly int VignetteIntensityID = Shader.PropertyToID("_VignetteIntensity");
        private static readonly int VignettePowerID = Shader.PropertyToID("_VignettePower");
        private static readonly int ScaleID = Shader.PropertyToID("_Scale");

        [Header("Vignette Settings")] [SerializeField]
        private float vignettePower = 3f;

        [SerializeField] private float maxVignetteIntensity = 1f;

        [Header("Scale")] [SerializeField] private float minScale = 0.5f;
        [SerializeField] private float maxScale = 1f;

        [Header("Pulse")] [SerializeField] private float pulseSpeed = 1f;

        [Header("Smooth Settings")] [SerializeField]
        private float smoothSpeed = 3f;

        private float _currentIntensity;
        private float _targetIntensity;
        private float _currentScale;
        private float _targetScale;
        
        private void Update()
        {
            if (!IsEnabled())
                return;
            UpdateTargetsValues();
            SmoothIntensity();
            SmoothScale();
            ApplyRuntimeParams();
        }

        protected override void InitParams()
        {
            _currentIntensity = 0f;
            _targetIntensity = 0f;
            _currentScale = minScale;
            _targetScale = minScale;
            _material.SetFloat(VignetteIntensityID, _currentIntensity);
            _material.SetFloat(ScaleID, _currentScale);
        }

        private void UpdateTargetsValues()
        {
            if (IsBarrierDestroyed() || IsNotInDangerThreshold())
            {
                SetDisabled();
                _targetIntensity = 0f;
                _targetScale = minScale;
                _material.SetFloat(VignetteIntensityID, _targetIntensity);
                _material.SetFloat(ScaleID, _targetScale);
                return;
            }

            var t = 1f - _barrierValue / _appearThreshold;
            _targetIntensity = Mathf.Clamp01(t) * maxVignetteIntensity;
            _targetScale = Mathf.Lerp(minScale, maxScale, t);
        }

        private void SmoothIntensity()
        {
            if (IsNearTargetValue(_currentIntensity, _targetIntensity))
                return;
            _currentIntensity = Mathf.Lerp(
                _currentIntensity,
                _targetIntensity,
                Time.deltaTime * smoothSpeed
            );
        }

        private void SmoothScale()
        {
            if (IsNearTargetValue(_currentScale, _targetScale))
                return;
            _currentScale = Mathf.Lerp(_currentScale, _targetScale, Time.deltaTime * smoothSpeed);
        }

        protected override void ApplyRuntimeParams()
        {
            if (!_material || !IsEnabled()) return;

            _material.SetFloat(VignetteIntensityID, _currentIntensity);
            _material.SetFloat(ScaleID, _currentScale);
        }

        protected override void ApplyStaticParams()
        {
            if (!_material) return;

            _material.SetFloat(VignettePowerID, vignettePower);
            _material.SetFloat(PulseSpeedID, pulseSpeed);
        }

        private void OnValidate()
        {
            ApplyStaticParams();
        }
    }
}