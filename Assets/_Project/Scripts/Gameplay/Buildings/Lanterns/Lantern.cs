using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(ResourceProducer))]
    public class Lantern : MonoBehaviour
    {

        [field: SerializeField]
        private SpriteRenderer UnFired { get; set; }

        [field: SerializeField]
        private SpriteRenderer Fired { get; set; }

        private ResourceProducer _producer;
        
        private bool _isFired;
        private int _currentResourceGeneration;
        private int _maxResourceGenerationPerFireUp = 3;

        public Action<Lantern> OnDestroyed = delegate { };
        public Action<Lantern> OnNeededToFire = delegate { };
        public Action<Lantern> OnFired = delegate { };
        public Action OnFireOff = delegate { };


        public void Init(int maxResourceGenerate)
        {
            _maxResourceGenerationPerFireUp = maxResourceGenerate;
            _currentResourceGeneration = maxResourceGenerate;
        }

        private void Awake()
        {
            _producer = GetComponent<ResourceProducer>();
        }

        public void Start()
        {
            _producer.OnProduced += DecrementFireCount;
        }

        private void OnDestroy()
        {
            _producer.OnProduced -= DecrementFireCount;
            OnDestroyed.Invoke(this);
        }

        private void DecrementFireCount(int _)
        {
            _currentResourceGeneration -= 1;
            if (_currentResourceGeneration <= 0)
                FireOff();
        }

        public bool IsFired()
        {
            return _isFired;
        }

        public void FireUp()
        {
            _isFired = true;
            _currentResourceGeneration = _maxResourceGenerationPerFireUp;

            UnFired.gameObject.SetActive(false);
            Fired.gameObject.SetActive(true);
            OnFired.Invoke(this);
        }

        private void FireOff()
        {
            _isFired = false;

            UnFired.gameObject.SetActive(true);
            Fired.gameObject.SetActive(false);

            OnFireOff.Invoke();

            OnNeededToFire.Invoke(this);
        }

    }
}