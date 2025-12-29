using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IWorkers
    {
        public event Action<Unit> OnUnitAdded;
        public event Action<Unit> OnUnitRemoved;

        List<Unit> CurWorkers { get; }
        int MaxCount { get; }
        int CurrentCount { get; }

        void Init(int maxValue);
        void AddWorker(Unit specUnit);
        void RemoveWorker(out Unit worker);
        bool CanAddWorker();
        bool HasAnyWorker();
    }
}