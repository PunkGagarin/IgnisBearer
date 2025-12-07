using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(Lantern))]
    [RequireComponent(typeof(ILightStorage))]
    public class LanternLightProducer : MonoBehaviour
    {
        [Inject] private LanternSettings _settings;

        private ILightStorage _lightStorage;
        private Lantern _lantern;

        private bool _isProducing;

        public event Action<float> OnLightProgressed = delegate { };

        private void Awake()
        {
            _lightStorage = GetComponent<ILightStorage>();
            _lantern = GetComponent<Lantern>();
        }


        private void Update()
        {
            if (_lantern.IsFired() && _lightStorage.NotFull() && !IsProducing())
                ProduceLight().Forget();
        }

        private async UniTaskVoid ProduceLight()
        {
            _isProducing = true;
            float estimatedTime = 0;

            while (estimatedTime < _settings.LightProduceTime)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                estimatedTime += Time.deltaTime;
                OnLightProgressed(estimatedTime / _settings.LightProduceTime);
            }

            _lightStorage.IncrementAmount();
            Debug.Log("Lantern produced " + 1);

            _isProducing = false;
        }

        public bool IsProducing()
            => _isProducing;
    }
}