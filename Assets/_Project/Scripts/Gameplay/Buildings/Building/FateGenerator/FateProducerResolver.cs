using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    [RequireComponent(typeof(IWorkers))]
    [RequireComponent(typeof(IResourceStorage))]
    public class FateProducerResolver : BaseProducerResolver
    {
        private Workers _workers;
        private IResourceStorage _iResourceStorage;

        protected void Awake()
        {
            _workers = GetComponent<Workers>();
            _iResourceStorage = GetComponent<IResourceStorage>();
        }

        public override bool CanProduce()
        {
            return _workers.HasAnyWorker() && _iResourceStorage.NotFull();
        }
    }
}