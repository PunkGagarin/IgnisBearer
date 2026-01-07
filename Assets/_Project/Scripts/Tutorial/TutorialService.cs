using Zenject;

namespace _Project.Scripts.Tutorial
{
    public class TutorialService
    {
        [Inject] private TutorialUi _ui;
        [Inject] private TutorialStepFactory _factory;

        private const TutorStepType FirstStep = TutorStepType.BuyUnit;

        private ITutorialStep _tutorialStep;

        public void StartTutor()
        {
            CreateAndStartStep(FirstStep);
        }

        private void CreateAndStartStep(TutorStepType step)
        {
            _tutorialStep = _factory.CreateStep(step);
            _tutorialStep.StartStep();
            _tutorialStep.OnFinishStep += SetNextStep;
        }

        private void SetNextStep(TutorStepType nextStep)
        {
            _tutorialStep.OnFinishStep -= SetNextStep;

            if (nextStep != TutorStepType.None)
                CreateAndStartStep(nextStep);
        }
    }
}