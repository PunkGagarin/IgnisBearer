using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(LanternClickDetector))]
    [RequireComponent(typeof(IResourceStorage))]
    public class Lantern : MonoBehaviour
    {
        private bool _isFired;

        [field: SerializeField]
        private SpriteRenderer UnFired { get; set; }

        [field: SerializeField]
        private SpriteRenderer Fired { get; set; }

        public Action<Lantern> OnDestroyed = delegate { };
        public Action<Lantern> OnNeededToFire = delegate { };
        public Action OnFired = delegate { };
        public Action OnFireOff = delegate { };

        private LanternUi _ui;
        private IResourceStorage _lightStorage;

        private int _currentHarvestCount;
        private int _maxHarvestCount = 3;

        public void Init(int maxHarvestCount)
        {
            _maxHarvestCount = maxHarvestCount;
            _currentHarvestCount = maxHarvestCount;
        }

        private void Awake()
        {
            _ui = GetComponent<LanternUi>();
            _lightStorage = GetComponent<IResourceStorage>();
        }

        public void Start()
        {
            _lightStorage.OnStorageCleared += DecrementFireCount;
        }

        private void OnDestroy()
        {
            _lightStorage.OnStorageCleared -= DecrementFireCount;
            OnDestroyed.Invoke(this);
        }

        private void DecrementFireCount()
        {
            _currentHarvestCount -= 1;
            if (_currentHarvestCount <= 0)
                FireOff();
        }

        public bool IsFired()
        {
            return _isFired;
        }

        public void FireUp()
        {
            _isFired = true;
            _currentHarvestCount = _maxHarvestCount;

            UnFired.gameObject.SetActive(false);
            Fired.gameObject.SetActive(true);
            OnFired.Invoke();
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