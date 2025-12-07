using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(LanternClickDetector))]
    public class Lantern : MonoBehaviour
    {
        private bool _isFired;

        [field: SerializeField]
        private SpriteRenderer UnFired { get; set; }

        [field: SerializeField]
        private SpriteRenderer Fired { get; set; }

        public Action<Lantern> OnDestroyed = delegate { };
        public Action OnFired = delegate { };

        private LanternClickDetector _clickDetector;

        private void Awake()
        {
            _clickDetector = GetComponent<LanternClickDetector>();
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
            OnFired.Invoke();
            
            //todo: убрать на попдиску?
            _clickDetector.TurnOffClick();
        }

        public void FireOff()
        {
            _isFired = false;
            UnFired.gameObject.SetActive(true);
            Fired.gameObject.SetActive(false);
            _clickDetector.TurnOnClick();
        }

        private void OnDestroy()
        {
            OnDestroyed.Invoke(this);
        }
    }
}