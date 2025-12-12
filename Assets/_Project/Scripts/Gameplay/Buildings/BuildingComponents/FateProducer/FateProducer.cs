using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(IResourceStorage))]
    public class FateProducer : MonoBehaviour, IFateProducer
    {
        private IResourceStorage _storage;
        private IWorkers _workers;

        private bool _isProducing;
        private float _timeToProduce;
        private int _amountToProduceAtTime;

        public void Init(float timeToProduce, int amountToProduceAtTime)
        {
            _timeToProduce = timeToProduce;
            _amountToProduceAtTime = amountToProduceAtTime;
        }

        private void Awake()
        {
            _storage = GetComponent<IResourceStorage>();
            _workers = GetComponent<IWorkers>();
        }

        private void Update()
        {
            if (CanProduce())
                ProduceFate().Forget();
        }

        private bool CanProduce()
        {
            return HasUnits() && _storage.NotFull() && !IsProducing();
        }

        private bool HasUnits()
        {
            return _workers.HasAnyWorker();
        }

        public bool IsProducing() => _isProducing;

        private async UniTaskVoid ProduceFate()
        {
            _isProducing = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToProduce));

            _storage.IncrementAmount(_amountToProduceAtTime);
            // Debug.Log("Fate produced " + _amountToProduceAtTime);

            _isProducing = false;
        }
    }
}