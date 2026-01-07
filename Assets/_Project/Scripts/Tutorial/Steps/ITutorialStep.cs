namespace _Project.Scripts
{
    public interface ITutorialStep
    {
        void StartStep();
        void FinishStep();
        public TutorStepType NextStep { get; }
    }
}