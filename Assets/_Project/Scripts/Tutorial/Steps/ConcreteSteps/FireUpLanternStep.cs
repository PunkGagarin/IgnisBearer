using _Project.Scripts.Gameplay.Buildings.Lanterns;
using Zenject;

namespace _Project.Scripts.Tutorial
{
    public class FireUpLanternStep : BaseTutorialStep
    {
        public override TutorStepType NextStep { get; } = TutorStepType.SendLightToChurch;
        protected override string Text { get; set; } = "Зажги фонарь";

        [Inject] private LanternService _lanternService;

        protected override void Subscribe()
        {
            _lanternService.OnLanternFired += OnStepIterated;
        }

        private void OnStepIterated()
        {
            FinishStep();
        }

        protected override void Unsubscribe()
        {
            _lanternService.OnLanternFired -= OnStepIterated;
        }
    }
}