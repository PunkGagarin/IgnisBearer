using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using _Project.Scripts.Gameplay.Units;
using Zenject;

namespace _Project.Scripts.Gameplay.Tutorial
{
    public class SendUnitIntoChapelStep : BaseTutorialStep
    {
        public override TutorStepType NextStep => TutorStepType.BuySecondUnit;
        protected override string Text { get; set; } = "Назначь юнита в часовню";

        [Inject] private BuildingsService _buildingsService;

        protected override void Subscribe()
        {
            var building = _buildingsService.GetBuilding<FateGeneratorBuilding>();
            var workers = building.GetComponent<Workers>();
            workers.OnUnitAdded += OnStepIterated;

        }

        private void OnStepIterated(Unit unit)
        {
            FinishStep();
        }

        protected override void Unsubscribe()
        {
            var building = _buildingsService.GetBuilding<FateGeneratorBuilding>();
            var workers = building.GetComponent<Workers>();
            workers.OnUnitAdded -= OnStepIterated;
        }
    }
}