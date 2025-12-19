using System;
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
        private int amountPerWorker = 1;

        private void Awake()
        {
             _workers = GetComponent<Workers>();
             _resourceProducer = GetComponent<ResourceProducer>();
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

        private void UpdateAmountProduced(Unit _)
        {
            int workersCurrentCount = _workers.CurrentCount;
            int totalAmount = workersCurrentCount * amountPerWorker;
            _resourceProducer.SetAmountToProduce(totalAmount);
        }
    }
}