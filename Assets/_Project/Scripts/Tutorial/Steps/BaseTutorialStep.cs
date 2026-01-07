using System;
using Zenject;

namespace _Project.Scripts.Tutorial
{
    public abstract class BaseTutorialStep : ITutorialStep
    {
        [Inject] private TutorialUi _ui;

        public event Action<TutorStepType> OnFinishStep = delegate { };

        public abstract TutorStepType NextStep { get; }

        protected abstract string Text { get; set; }

        public virtual void StartStep()
        {
            Subscribe();
            _ui.SetTutorialText(Text);
        }

        protected abstract void Subscribe();

        public virtual void FinishStep()
        {
            Unsubscribe();
            _ui.Hide();
            OnFinishStep.Invoke(NextStep);
        }

        protected abstract void Unsubscribe();
    }
}