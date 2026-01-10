using System;

namespace _Project.Scripts.Tutorial
{
    public interface ITutorialStep
    {
        void StartStep();
        public TutorStepType NextStep { get; }
        public event Action<TutorStepType> OnFinishStep;
    }
}