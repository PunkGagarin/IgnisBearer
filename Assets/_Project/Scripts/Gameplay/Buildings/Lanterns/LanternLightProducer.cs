using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternLightProducer : MonoBehaviour
    {
        private LanternLightStorage _lanternLightStorage;
        private Lantern _lantern;

        private bool _isProducing;

        private void Awake()
        {
            _lanternLightStorage = GetComponent<LanternLightStorage>();
            _lantern = GetComponent<Lantern>();
        }


        private void Update()
        {
            if (_lantern.IsFired() && _lanternLightStorage.NotFull() && !IsProducing())
                ProduceLight().Forget();
        }

        public async UniTaskVoid ProduceLight()
        {
            _isProducing = true;
            await UniTask.Delay(TimeSpan.FromSeconds(5f));

            _lanternLightStorage.IncrementCurrentAmount();
            Debug.Log("Lantern produced " + 1);

            _isProducing = false;
        }

        public bool IsProducing()
            => _isProducing;
    }
}