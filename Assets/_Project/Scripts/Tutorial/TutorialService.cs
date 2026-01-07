using Zenject;

namespace _Project.Scripts.Tutorial
{
    public class TutorialService
    {
        [Inject] private TutorialUi _ui;
        [Inject] private TutorialStepFactory _factory;

        private const TutorStepType FirstStep = TutorStepType.BuyUnit;

        private ITutorialStep _tutorialStep;

        // купить доп юнита
        // флоу - все действия необходимые по тутору
        // цели - конечные цели тутора


        public void StartTutor()
        {
            _tutorialStep = _factory.CreateStep(FirstStep);
            _tutorialStep.StartStep();
        }
    }
}