using UnityEngine;

namespace _Project.Scripts.Shaders
{
    public class VignetteShaderController : MonoBehaviour
    {
        private static readonly int PulseSpeedID = Shader.PropertyToID("_PulseSpeed");
        private static readonly int VignetteIntensityID = Shader.PropertyToID("_VignetteIntensity");
        private static readonly int VignettePowerID = Shader.PropertyToID("_VignettePower");
        private static readonly int ScaleID = Shader.PropertyToID("_Scale");

        [Header("Material")]
        [SerializeField] private Material material;

        [Tooltip("At this value vignette starts appearing")]
        [Range(0, 100)]
        [SerializeField] private float dangerThreshold = 30f;

        [Header("Vignette Settings")]
        [SerializeField] private float vignettePower = 3f;
        [SerializeField] private float maxVignetteIntensity = 1f;

        [Header("Scale")]
        [SerializeField] private float minScale = 0.5f;
        [SerializeField] private float maxScale = 1f;

        [Header("Pulse")]
        [SerializeField] private float pulseSpeed = 1f;

        [Header("Smooth Settings")]
        [SerializeField] private float smoothSpeed = 3f;

        private float _currentIntensity;
        private float _targetIntensity;
        private float _currentScale;
        private float _targetScale;
        private float _barrierValue;

        private void Awake()
        {
            InitParams();
            ApplyStaticParams();
            ApplyRuntimeParams();
        }

        private void Update()
        {
            UpdateTargetsValues();
            SmoothIntensity();
            SmoothScale();
            ApplyRuntimeParams();
        }

        private void InitParams()
        {
            _currentIntensity = 0f;
            _targetIntensity = 0f;
            _currentScale = minScale;
            _targetScale = minScale;
        }

        public void SetBarrierValue(float value)
        {
            _barrierValue = value;
        }

        private void UpdateTargetsValues()
        {
            if (IsBarrierDestroyed() || IsInDangerThreshold())
            {
                _targetIntensity = 0f;
                _targetScale = minScale;
                return;
            }

            var t = 1f - _barrierValue / dangerThreshold;
            _targetIntensity = Mathf.Clamp01(t) * maxVignetteIntensity;
            _targetScale = Mathf.Lerp(minScale, maxScale, t);
        }

        private bool IsInDangerThreshold()
        {
            return _barrierValue > dangerThreshold;
        }

        private bool IsBarrierDestroyed()
        {
            return _barrierValue <= 0f;
        }

        private void SmoothIntensity()
        {
            if (IsNearTargetIntensity())
                return;
            _currentIntensity = Mathf.Lerp(
                _currentIntensity,
                _targetIntensity,
                Time.deltaTime * smoothSpeed
            );
        }

        private void SmoothScale()
        {
            if (IsNearTargetScale())
                return;
            _currentScale = Mathf.Lerp(_currentScale, _targetScale, Time.deltaTime * smoothSpeed);
        }

        private bool IsNearTargetScale()
        {
            return Mathf.Approximately(_currentScale, _targetScale);
        }

        private bool IsNearTargetIntensity()
        {
            return Mathf.Approximately(_currentIntensity, _targetIntensity);
        }

        private void ApplyRuntimeParams()
        {
            if (!material) return;

            material.SetFloat(VignetteIntensityID, _currentIntensity);
            material.SetFloat(ScaleID, _currentScale);
        }

        private void ApplyStaticParams()
        {
            if (!material) return;

            material.SetFloat(VignettePowerID, vignettePower);
            material.SetFloat(PulseSpeedID, pulseSpeed);
        }

        private void OnValidate()
        {
            ApplyStaticParams();
        }
    }
}
