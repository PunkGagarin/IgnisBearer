using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class Workers : MonoBehaviour, IWorkers
    {
        public event Action<Unit> OnUnitAdded = delegate { };
        public event Action<Unit> OnUnitRemoved = delegate { };
        public event Action<int> OnMaxCountChanged = delegate { };
        public List<Unit> CurWorkers { get; } = new();

        public int CurrentCount => CurWorkers.Count;
        public int MaxCount { get; private set; }

        public void Init(int maxValue)
        {
            MaxCount = maxValue;
            OnMaxCountChanged?.Invoke(MaxCount);
        }

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

            unit = CurWorkers.RandomOrDefault(el => el.Context.Status == UnitStatus.Free);
            return unit != null;
        }


        public void RemoveWorker(out Unit worker)
        {
            worker = null;
            if (HasAnyWorker())
            {
                //todo: worker вот тут потенциальная бага: не проверяет
                //свободен ли юнит а просто снимает его,юнит может не закончить текущее дейтвие
                worker = CurWorkers.First();
                CurWorkers.Remove(worker);
                OnUnitRemoved?.Invoke(worker);
            }
        }
    }
}