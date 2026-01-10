using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Ui.Buildings;
using Zenject;

namespace _Project.Scripts.Tutorial
{
    public class BuyUnitStep : BaseTutorialStep
    {
        public override TutorStepType NextStep { get; } = TutorStepType.FireUpLantern;
        protected override string Text { get; set; } = "Купить юнита в доме";

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
    