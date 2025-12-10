using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class Workers : MonoBehaviour, IWorkers
    {
        public event Action<List<Unit>> ListChanged;
        public event Action<int> CountChanged;
        public event Action<int> MaxCountChanged;

        public List<Unit> CurWorkers { get; set; } = new();
        public int CurrentCount { get; set; }

        public int MaxCount { get; set; }

        public void Init(int initValue, int maxValue)
        {
            CurrentCount = initValue;
            MaxCount = maxValue;
        }

        public void UpdateMaxCount(int count)
        {
            MaxCount = count;
            MaxCountChanged?.Invoke(count);
        }

        private void IncrementCount() => UpdateCount(CurrentCount + 1);

        private void DecrementCount() => UpdateCount(CurrentCount - 1);

        public void UpdateCount(int count)
        {
            CurrentCount = count;
            CountChanged?.Invoke(count);
        }

        public bool CanAddWorker()
            => CurrentCount < MaxCount;

        public void AddWorker(Unit specUnit)
        {
            if (!CanAddWorker())
            {
                Debug.LogError("Workers capacity is full");
                return;
            }

            CurWorkers.Add(specUnit);
            IncrementCount();
            ListChanged?.Invoke(CurWorkers);
        }

        public bool HasAnyWorker() => CurrentCount != 0;

        public void RemoveWorker(out Unit worker)
        {
            worker = null;
            if (HasAnyWorker())
            {
                worker = CurWorkers.First();
                CurWorkers.Remove(worker);
                DecrementCount();
                ListChanged?.Invoke(CurWorkers);
            }
        }
    }
}