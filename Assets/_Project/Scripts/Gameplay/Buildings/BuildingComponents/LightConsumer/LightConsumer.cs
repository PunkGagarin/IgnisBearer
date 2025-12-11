using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class LightConsumer : MonoBehaviour, ILightConsumer
    {
        private ILightStorage _lightStorage;
        
        private float _timeToConsume;
        private int _amountToConsume;
        private bool _isConsuming;
        
        private void Awake()
        {
            _lightStorage = GetComponent<ILightStorage>();
        }

        private void Update()
        {
            if (CanConsume())
                ConsumeLight().Forget();
        }

        public void Init(float timeToConsume, int amountToConsume)
        {
            _timeToConsume = timeToConsume;
            _amountToConsume = amountToConsume;
        }

        private bool CanConsume()
        {
            return _lightStorage.HasAny() && !_isConsuming;
        }

        private async UniTaskVoid ConsumeLight()
        {
            _isConsuming = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToConsume));

            _lightStorage.DecrementAmount(_amountToConsume);

            _isConsuming = false;
        }
    }
}