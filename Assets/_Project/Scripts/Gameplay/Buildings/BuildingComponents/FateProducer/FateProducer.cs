using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(IFateStorage))]
    public class FateProducer : MonoBehaviour, IFateProducer
    {
        private IFateStorage _fateStorage;

        private bool _isProducing;
        private float _timeToProduce;
        private int _amountToProduceAtTime;

        private void Awake()
        {
            _fateStorage = GetComponent<IFateStorage>();
        }

        private void Update()
        {
            if (CanProduce())
                ProduceFate().Forget();
        }

        private bool CanProduce()
        {
            return _fateStorage.NotFull() && !IsProducing();
        }

        public void Init(float timeToProduce, int amountToProduceAtTime)
        {
            _timeToProduce = timeToProduce;
            _amountToProduceAtTime = amountToProduceAtTime;
        }

        public bool IsProducing() => _isProducing;

        private async UniTaskVoid ProduceFate()
        {
            _isProducing = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToProduce));

            _fateStorage.IncrementAmount(_amountToProduceAtTime);
            Debug.Log("Fate produced " + _amountToProduceAtTime);

            _isProducing = false;
        }
    }
}