using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public class HarvestLanternState : IUnitState, IPayloadState<TemporalLantern>
    {
        private Unit _unit;


        public void Init(Unit unit)
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