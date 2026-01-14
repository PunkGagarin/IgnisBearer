using _Project.Scripts.Gameplay.Buildings;
using Zenject;

namespace _Project.Scripts.Gameplay.Tutorial
{
    public class SendLightToChurchStep : BaseTutorialStep
    {
        public override TutorStepType NextStep => TutorStepType.BuyChapel;
        protected override string Text { get; set; } = "Отправь в церковь {0} ресурса";

        private int _currentRes;
        private int _targetRes;

        [Inject] private BuildingsService _buildingsService;
        [Inject] private BuildingSlotsService _buildingSlotsService;
        [Inject] private TutorialSettings _tutorialSettings;

        protected override void Subscribe()
        {
            var church = _buildingsService.GetChurch();
            var lightResourceStorage = church.GetComponent<IResourceStorage>();
            lightResourceStorage.OnAmountIncreased += OnStepIterated;

            _targetRes = _tutorialSettings.LightCountForChurch;
            Text = string.Format(Text, _targetRes);
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