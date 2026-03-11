using UnityEngine;

namespace _Project.Scripts.Shaders
{
    public abstract class BarrierVfxController : MonoBehaviour
    {
        [Header("Material")] 
        [SerializeField] protected Material _material;

        [Tooltip("At this value vignette starts appearing")]
        [Range(0, 100)]
        [SerializeField] protected float _appearThreshold;

        private bool _isEnabled;
        protected float _barrierValue;

        protected virtual void Awake()
        {
            InitParams();
            ApplyStaticParams();
            ApplyRuntimeParams();
        }

        public bool IsEnabled() => _isEnabled;

        protected void SetDisabled() => _isEnabled = false;

        protected void SetEnabled() => _isEnabled = true;

        protected bool IsNearTargetValue(float cur, float target)
        {
            return Mathf.Approximately(cur, target);
        }

        protected bool IsNotInDangerThreshold()
        {
            return _barrierValue >= _appearThreshold;
        }

        protected bool IsBarrierDestroyed()
        {
            return _barrierValue <= 0f;
        }

        public void SetBarrierValue(float value)
        {
            SetEnabled();
            _barrierValue = value;
        }

        protected virtual void ApplyStaticParams() {}
        protected virtual void ApplyRuntimeParams() {}
        
        protected abstract void InitParams();
    }
}