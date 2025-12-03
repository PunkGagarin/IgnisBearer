using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public class HarvestLanternState : IUnitState, IPayloadState<TemporalLantern>
    {
        private readonly Unit _unit;

        public HarvestLanternState(Unit unit)
        {
            _unit = unit;
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Enter(TemporalLantern payload)
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            _unit.Context.Status = UnitStatus.Free;
        }
    }
}