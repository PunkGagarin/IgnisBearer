using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using Zenject;

namespace _Project.Scripts.Gameplay.Tutorial
{
    public class BuyChapelStep : BaseTutorialStep
    {
        public override TutorStepType NextStep => TutorStepType.SendUnitIntoChapel;
        protected override string Text { get; set; } = "Построй часовню";

        [Inject] private BuildingsService _buildingsService;

        protected override void Subscribe()
        {
            if (_buildingsService.GetBuilding<FateGeneratorBuilding>() != null)
            {
                FinishStep();
                return;
            }

            _buildingsService.OnFateGeneratorBuilt += OnStepIterated;
        }

        private void OnStepIterated(FateGeneratorBuilding fateGeneratorBuilding)
        {
            FinishStep();
        }

        protected override void Unsubscribe()
        {
            _buildingsService.OnFateGeneratorBuilt -= OnStepIterated;
        }
    }
}