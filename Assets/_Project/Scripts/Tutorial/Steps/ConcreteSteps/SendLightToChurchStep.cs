using _Project.Scripts.Gameplay.Buildings;
using Zenject;

namespace _Project.Scripts.Tutorial
{
    public class SendLightToChurchStep : BaseTutorialStep
    {
        public override TutorStepType NextStep => TutorStepType.BuyChapel;
        protected override string Text { get; set; } = "Отправь в церковь 3 ресурса";

        private int _currentRes;
        private int _targetRes = 3;

        [Inject] private BuildingsService _buildingsService;

        protected override void Subscribe()
        {
            var church = _buildingsService.GetChurch();
            var lightResourceStorage = church.GetComponent<IResourceStorage>();
            lightResourceStorage.OnAmountIncreased += OnStepIterated;
        }

        private void OnStepIterated((int amountIncreased, int newAmount, int maxAmount) valueTuple)
        {
            _currentRes++;
            if (_currentRes >= _targetRes)
                FinishStep();
        }

        protected override void Unsubscribe()
        {
            var church = _buildingsService.GetChurch();
            var lightResourceStorage = church.GetComponent<IResourceStorage>();
            lightResourceStorage.OnAmountIncreased -= OnStepIterated;
        }
    }
}