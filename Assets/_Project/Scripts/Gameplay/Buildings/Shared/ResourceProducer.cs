using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{

    [RequireComponent(typeof(IResourceStorage))]
    public class ResourceProducer : MonoBehaviour
    {
        private IResourceStorage _iResourceStorage;

        private bool _isProducing;
        private float _timeToProduce;

        public bool CanProduce { get; set; }

        public event Action<float> OnLightProgressed = delegate { };
        public event Action OnStartProducing = delegate { };
        public event Action OnEndProducing = delegate { };

        public void Init(float produceTime)
        {
            _timeToProduce = produceTime;
        }

        private void Awake()
        {
            _iResourceStorage = GetComponent<IResourceStorage>();
        }

        private void Update()
        {
            if (CanProduce)
                Produce().Forget();
        }

        private async UniTaskVoid Produce()
        {
            _isProducing = true;
            OnStartProducing.Invoke();
            float estimatedTime = 0;

            while (estimatedTime < _timeToProduce)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                estimatedTime += Time.deltaTime;
                OnLightProgressed(estimatedTime / _timeToProduce);
            }

            _iResourceStorage.IncrementAmount();

            _isProducing = false;
            OnEndProducing.Invoke();
        }

        public bool IsProducing()
            => _isProducing;

    }
}