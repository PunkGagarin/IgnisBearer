using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class BaseProducerSwitcher : MonoBehaviour
    {
        private ResourceProducer _resourceProducer;
        private IResourceStorage _iResourceStorage;


        protected virtual void Awake()
        {
            _iResourceStorage = GetComponent<IResourceStorage>();
            _resourceProducer = GetComponent<ResourceProducer>();
        }

        private void Update()
        {
            _resourceProducer.CanProduce = CanProduce();
        }

        private bool CanProduce()
        {
            return IsReadyToProduce() && _iResourceStorage.NotFull() && !_resourceProducer.IsProducing();
        }

        protected abstract bool IsReadyToProduce();
    }
}