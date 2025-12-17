using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class LightConsumer : MonoBehaviour, ILightConsumer
    {
        private IResourceStorage _iResourceStorage;
        
        private float _timeToConsume;
        private int _amountToConsume;
        private bool _isConsuming;

        public bool IsConsumeStarted { get; set; }

        private void Awake()
        {
            _iResourceStorage = GetComponent<IResourceStorage>();
        }

        private void Update()
        {
            if (IsConsumeStarted && CanConsume())
                ConsumeLight().Forget();
        }

        public void Init(float timeToConsume, int amountToConsume)
        {
            _timeToConsume = timeToConsume;
            _amountToConsume = amountToConsume;
        }

        private bool CanConsume()
        {
            return _iResourceStorage.HasAny() && !_isConsuming;
        }

        private async UniTaskVoid ConsumeLight()
        {
            _isConsuming = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToConsume));

            _iResourceStorage.DecrementAmount(_amountToConsume);

            _isConsuming = false;
        }
    }
}