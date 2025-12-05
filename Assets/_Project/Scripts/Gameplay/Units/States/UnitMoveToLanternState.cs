using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToLanternState : IUnitState, IPayloadState<Lantern>
    {
        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter(Lantern lantern)
        {
            _unit.Context.Status = UnitStatus.Busy;

            await _unit.Mover.MoveTo(_unit.Context.MoveTarget);

            if (IsNeedToFireUpLantern(lantern))
                _unit.StateMachine.Enter<FireUpLanternState, Lantern>(lantern);
            else if (IsLanternReadyToHarvest(lantern))
                _unit.StateMachine.Enter<HarvestLanternState, Lantern>(lantern);
        }

        private bool IsLanternReadyToHarvest(Lantern lantern)
        {
            var lanternResource = lantern.GetComponent<LanternResource>();
            return lanternResource.IsReadyToHarvest();
        }

        private bool IsNeedToFireUpLantern(Lantern lantern)
        {
            return !lantern.IsFired();
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }

}