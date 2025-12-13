using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IWorkers
    {
        public event Action<Unit> OnUnitAdded;
        public event Action<Unit> OnUnitRemoved;
        event Action<int> CountChanged;
        event Action<int> MaxCountChanged;

        List<Unit> CurWorkers { get; set; }
        int MaxCount { get; set; }
        int CurrentCount { get; set; }

        void Init(int initValue, int maxValue);
        void AddWorker(Unit specUnit);
        void RemoveWorker(out Unit worker);
        void UpdateMaxCount(int count);
        void UpdateCount(int count);
        bool CanAddWorker();
        bool HasAnyWorker();
    }
}