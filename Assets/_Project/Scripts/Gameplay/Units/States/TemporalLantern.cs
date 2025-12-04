using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class TemporalLantern : MonoBehaviour
    {
        
        private bool _isFired;
        private bool _isReadyToHarvest = true;

        [field: SerializeField]
        private SpriteRenderer UnFired { get; set; }

        [field: SerializeField]
        private SpriteRenderer Fired { get; set; }
        
        public Action<TemporalLantern> OnDestroyed = delegate { };
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool IsReadyToHarvest()
        {
            return _isReadyToHarvest;
        }

        public bool IsFired()
        {
            return _isFired;
        }
        
        public void FireUp()
        {
            _isFired = true;
            UnFired.gameObject.SetActive(false);
            Fired.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            OnDestroyed.Invoke(this);
        }
    }
}