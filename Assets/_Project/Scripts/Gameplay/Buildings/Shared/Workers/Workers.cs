using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class Workers : MonoBehaviour, IWorkers
    {
        public event Action<Unit> OnUnitAdded = delegate { };
        public event Action<Unit> OnUnitRemoved = delegate { };
        public List<Unit> CurWorkers { get; set; } = new();
        
        public int CurrentCount => CurWorkers.Count;
        public int MaxCount { get; set; }

        public void Init(int maxValue) => MaxCount = maxValue;

        public bool CanAddWorker() => CurrentCount < MaxCount;

        public void AddWorker(Unit specUnit)
        {
            if (!CanAddWorker())
            {
                Debug.LogError("Workers capacity is full");
                return;
            }

            CurWorkers.Add(specUnit);
            OnUnitAdded?.Invoke(specUnit);
        }

        public bool HasAnyWorker() => CurrentCount != 0;

        public bool HasAnyFreeWorker(out Unit unit)
        {
            unit = null;

            if (CurrentCount == 0)
                return false;

            for (int index = 0; index < CurWorkers.Count; index++)
            {
                unit = CurWorkers[index];
                if (unit.Context.Status == UnitStatus.Free)
                {
                    unit = CurWorkers[index];
                    return true;
                }
            }

            return false;
        }


        public void RemoveWorker(out Unit worker)
        {
            worker = null;
            if (HasAnyWorker())
            {
                worker = CurWorkers.First();
                CurWorkers.Remove(worker);
                OnUnitRemoved?.Invoke(worker);
            }
        }
    }
}