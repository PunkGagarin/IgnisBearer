using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Ui.Buildings;
using Zenject;

namespace _Project.Scripts.Gameplay.Tutorial
{
    public class BuySecondUnitStep : BaseTutorialStep
    {
        public override TutorStepType NextStep => TutorStepType.None;
        protected override string Text { get; set; } = "Купи второго юнита";

        [Inject] private BuildingsService _buildingsService;

        protected override void Subscribe()
        {
            var house = _buildingsService.GetBuilding<HouseBuilding>();
            var houseUnits = house.GetComponent<HouseBuyUnit>();

            houseUnits.OnUnitCountChanged += OnUnitChangedHandle;
        }

        private void OnUnitChangedHandle(int obj)
        {
            FinishStep();
        }

        protected override void Unsubscribe()
        {
            var house = _buildingsService.GetBuilding<HouseBuilding>();
            var houseUnits = house.GetComponent<HouseBuyUnit>();

            houseUnits.OnUnitCountChanged -= OnUnitChangedHandle;
        }
    }
}