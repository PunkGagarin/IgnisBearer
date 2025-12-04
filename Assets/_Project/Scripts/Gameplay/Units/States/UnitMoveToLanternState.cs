using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToLanternState : IUnitState, IPayloadState<TemporalLantern>
    {
        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter(TemporalLantern temporalLantern)
        {
            _unit.Context.Status = UnitStatus.Busy;

            await _unit.Mover.MoveTo(_unit.Context.MoveTarget);

            if (IsNeedToFireupLantern(temporalLantern))
                _unit.StateMachine.Enter<FireUpLanternState, TemporalLantern>(temporalLantern);
            else if (IsLanternReadyToHarvest(temporalLantern))
                _unit.StateMachine.Enter<HarvestLanternState, TemporalLantern>(temporalLantern);
        }

        private bool IsLanternReadyToHarvest(TemporalLantern temporalLantern)
        {
            return temporalLantern.IsReadyToHarvest();
        }

        private bool IsNeedToFireupLantern(TemporalLantern temporalLantern)
        {
            return !temporalLantern.IsFired();
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }

}