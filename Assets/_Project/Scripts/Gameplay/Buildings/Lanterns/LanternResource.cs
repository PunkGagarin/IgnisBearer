using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(Lantern))]
    [RequireComponent(typeof(LanternClickDetector))]
    public class LanternResource : MonoBehaviour
    {
        private Lantern _lantern;
        private LanternClickDetector _clickDetector;

        private int _currentAmount;
        private int _maxAmount = 1;
        private bool _isProducing;


        private void Awake()
        {
            _lantern = GetComponent<Lantern>();
            _clickDetector = GetComponent<LanternClickDetector>();
        }

        private void Update()
        {
            if (_lantern.IsFired() && NotFull() && !_isProducing)
                ProduceLight().Forget();
        }

        private bool NotFull()
        {
            return _currentAmount < _maxAmount;
        }

        private async UniTaskVoid ProduceLight()
        {
            _isProducing = true;
            await UniTask.Delay(TimeSpan.FromSeconds(5f));

            _currentAmount++;
            _clickDetector.TurnOnClick();

            Debug.Log("Lantern produced " + _currentAmount);
            _isProducing = false;
        }

        public bool IsReadyToHarvest()
        {
            return IsFull();
        }

        private bool IsFull()
        {
            return !NotFull();
        }

        public int Harvest()
        {
            int amount = _currentAmount;
            Debug.Log("Lantern harvested " + _currentAmount);

            _currentAmount = 0;
            _clickDetector.TurnOffClick();
            return amount;
        }
    }
}