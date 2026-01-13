using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(IProduceResolver))]
    public class ResourceProducer : MonoBehaviour
    {
        private bool _isProducing;
        private float _timeToProduce;
        private int _amountToProduce = 1;

        private IProduceResolver _resolver;

        public event Action<float> OnLightProgressed = delegate { };
        public event Action OnStartProducing = delegate { };
        public event Action<int> OnProduced = delegate { };

        private void Awake()
        {
            _resolver = GetComponent<IProduceResolver>();
        }

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
            if (_resolver.CanProduce() && !_isProducing)
                Produce().Forget();
        }

        private async UniTaskVoid Produce()
        {
            try
            {
                _isProducing = true;
                OnStartProducing.Invoke();
                float estimatedTime = 0;

                while (estimatedTime < _timeToProduce)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: destroyCancellationToken);
                    estimatedTime += Time.deltaTime;
                    OnLightProgressed(estimatedTime / _timeToProduce);
                }

                if (_resolver.CanProduce())
                    OnProduced.Invoke(_amountToProduce);
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _isProducing = false;
            }
        }
    }
}