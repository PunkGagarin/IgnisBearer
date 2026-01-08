using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Infrastructure.GameStates;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitAddToChurchQueueState : IUnitState, IState
    {
        private Unit _unit;
        
        [Inject] private BuildingsService _buildingsService;
        [Inject] private UnitSettings _unitSettings;

        private ChurchQueue ChurchQueue => _buildingsService.GetChurch().GetComponent<ChurchQueue>();
        
        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Enter()
        {
            ChurchQueue.AddUnitForQueue(_unit);
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}