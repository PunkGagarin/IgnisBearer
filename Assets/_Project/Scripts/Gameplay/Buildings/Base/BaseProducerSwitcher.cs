using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent( typeof(ResourceProducer))]
    public abstract class BaseProducerSwitcher : MonoBehaviour
    {
        private ResourceProducer _resourceProducer;


        protected virtual void Awake()
        {
            _resourceProducer = GetComponent<ResourceProducer>();
        }

        private void Update()
        {
            _resourceProducer.CanProduce = CanProduce();
        }

        private bool CanProduce()
        {
            return IsReadyToProduce() && !_resourceProducer.IsProducing();
        }

        protected abstract bool IsReadyToProduce();
    }
}