using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    
    public class ResourceProducer : MonoBehaviour
    {
        private bool _isProducing;
        private float _timeToProduce;
        private int _amountToProduce = 1;

        public bool CanProduce { get; set; }

        public event Action<float> OnLightProgressed = delegate { };
        public event Action OnStartProducing = delegate { };
        public event Action<int> OnProduced = delegate { };

        public void Init(float produceTime)
        {
            _timeToProduce = produceTime;
        }

        public void SetAmountToProduce(int amount)
        {
            _amountToProduce = amount;
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

            _isProducing = false;
            OnProduced.Invoke(_amountToProduce);
        }

        public bool IsProducing()
            => _isProducing;
    }
}