using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(IWorkers))]
    [RequireComponent(typeof(ResourceProducer))]
    public class WorkerProduceAmountIncreaser : MonoBehaviour
    {
        private IWorkers _workers;
        private ResourceProducer _resourceProducer;
        private int _amountPerWorker;

        private void Awake()
        {
            _workers = GetComponent<Workers>();
            _resourceProducer = GetComponent<ResourceProducer>();
        }

        public void Init(int amountPerWorker)
        {
            _amountPerWorker = amountPerWorker;
            UpdateAmountProduced();
        }

        private void Start()
        {
            _workers.OnUnitAdded += UpdateAmountProduced;
            _workers.OnUnitRemoved += UpdateAmountProduced;
        }

        private void OnDestroy()
        {
            _workers.OnUnitAdded -= UpdateAmountProduced;
            _workers.OnUnitRemoved -= UpdateAmountProduced;
        }

        private void UpdateAmountProduced(Unit _) => UpdateAmountProduced();

        private void UpdateAmountProduced()
        {
            int workersCurrentCount = _workers.CurrentCount;
            int totalAmount = workersCurrentCount * _amountPerWorker;
            _resourceProducer.SetAmountToProduce(totalAmount);
        }
    }
}