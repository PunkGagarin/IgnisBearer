using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    [RequireComponent(typeof(IWorkers))]
    [RequireComponent(typeof(IResourceStorage))]
    public class FateProducerSwitcher : BaseProducerSwitcher
    {
        private Workers _workers;
        private IResourceStorage _iResourceStorage;

        protected override void Awake()
        {
            base.Awake();
            _workers = GetComponent<Workers>();
            _iResourceStorage = GetComponent<IResourceStorage>();
        }

        protected override bool IsReadyToProduce()
        {
            return _workers.HasAnyWorker() && _iResourceStorage.NotFull();
        }
    }
}