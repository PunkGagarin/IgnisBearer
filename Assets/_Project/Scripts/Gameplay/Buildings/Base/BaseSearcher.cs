using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class BaseSearcher<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected List<T> _resToServe = new();
        private Workers _workers;

        private void Awake()
        {
            _workers = GetComponent<Workers>();
        }

        private void Start()
        {
            _workers.OnUnitAdded += SubscribeUnit;
            _workers.OnUnitRemoved += UnsubscribeUnit;
            SubscribeToSearch();

            FindResForQueue();
        }

        protected abstract void SubscribeToSearch();

        protected void FindResForQueue()
        {
            var unservedRes = GetResForQueue();
            Debug.Log($" resources found for harvest: {unservedRes.Count}");
            foreach (var lantern in unservedRes)
                AddToQueue(lantern);
        }

        protected abstract List<T> GetResForQueue();

        private void AddToQueue(T res)
        {
            Debug.Log($" res added in harvest queue");
            if (!_resToServe.Contains(res))
                _resToServe.Add(res);
        }

        protected void OnDestroy()
        {
            foreach (var unit in _workers.CurWorkers)
                UnsubscribeUnit(unit);

            UnsubscribeFromRes();
        }

        protected abstract void UnsubscribeFromRes();

        private void SubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree += CheckResInQueue;
        }

        private void UnsubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree -= CheckResInQueue;
        }

        private void CheckResInQueue(Unit unit)
        {
            Debug.Log($" checking res in queue for free unit");
            if (_resToServe.Count > 0)
            {
                var res = _resToServe.FirstOrDefault();
                MoveTo(unit, res);
            }
        }

        protected abstract void MoveTo(Unit unit, T res);

        protected void SendFirstUnit(T res)
        {
            Debug.Log($" sending first free uit after res detected");
            if (!_workers.HasAnyFreeWorker(out var unit))
            {
                AddToQueue(res);
                return;
            }

            MoveTo(unit, res);
        }
    }
}