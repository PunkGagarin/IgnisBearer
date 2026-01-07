using _Project.Scripts.Gameplay.Buildings.Lanterns;
using Zenject;

namespace _Project.Scripts.Tutorial.Steps.ConcreteSteps
{
    public class FireUpLanternStep : BaseTutorialStep
    {
        
        public override TutorStepType NextStep { get; } = TutorStepType.None;
        protected override string Text { get; set; } = "Зажги фонарь";

        [Inject] private LanternService _lanternService;

        protected override void Subscribe()
        {
            // var house = _buildingsService.GetBuilding<HouseBuilding>();
            // var houseUnits = house.GetComponent<HouseBuyUnit>();
            //
            // houseUnits.OnUnitCountChanged += OnUnitChangedHandle;
        }

        private void OnUnitChangedHandle(int obj)
        {
            FinishStep();
        }

        protected override void Unsubscribe()
        {
            // var house = _buildingsService.GetBuilding<HouseBuilding>();
            // var houseUnits = house.GetComponent<HouseBuyUnit>();


        }
    }
}