using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class BaseLanternSearcher : MonoBehaviour
    {
        [Inject] protected readonly LanternService _lanternService;

        private Workers _workers;
        private Queue<Lantern> _lanternToServe = new();

        private void Awake()
        {
            _workers = GetComponent<Workers>();
        }

        private void Start()
        {
            _workers.OnUnitAdded += SubscribeUnit;
            _workers.OnUnitRemoved += UnsubscribeUnit;
            SubscribeOnLantern();

            FindLanternsForQueue();
        }

        protected abstract void SubscribeOnLantern();

        private void FindLanternsForQueue()
        {
            var unfiredLanterns = GetLanternsForQueue();
            Debug.Log($" lanterns found for harvest: {unfiredLanterns.Count}");
            foreach (var lantern in unfiredLanterns)
                AddLanternToQueue(lantern);
        }

        protected abstract List<Lantern> GetLanternsForQueue();

        private void AddLanternToQueue(Lantern lantern)
        {
            Debug.Log($" lantern added in harvest queue");
            _lanternToServe.Enqueue(lantern);
        }

        protected void OnDestroy()
        {
            foreach (var unit in _workers.CurWorkers)
                UnsubscribeUnit(unit);

            UnsubscribeFromLantern();
        }

        protected abstract void UnsubscribeFromLantern();

        private void SubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree += CheckLanternInQueue;
        }

        private void UnsubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree -= CheckLanternInQueue;
        }

        private void CheckLanternInQueue(Unit unit)
        {
            Debug.Log($" checking lantern in queue for free unit");
            if (_lanternToServe.Count > 0)
            {
                var lantern = _lanternToServe.Dequeue();
                MoveToLantern(unit, lantern);
            }
        }

        private void MoveToLantern(Unit unit, Lantern lantern)
        {
            unit.StateMachine.Enter<MoveToWithNextAndPayload, FireUpLanternState, Vector3, Lantern>(
                lantern.transform.position, lantern);
        }

        protected void SendFirstUnit(Lantern lantern)
        {
            Debug.Log($" sending first free uit after lantern full detected");
            if (!_workers.HasAnyFreeWorker(out var unit))
            {
                AddLanternToQueue(lantern);
                return;
            }
            MoveToLantern(unit, lantern);
        }
    }
}