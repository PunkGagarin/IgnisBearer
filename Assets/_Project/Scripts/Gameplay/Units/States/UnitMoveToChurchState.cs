using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToChurchState : IUnitState, IPayloadState<Vector3>
    {
        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter(Vector3 vector3)
        {
            _unit.Context.Status = UnitStatus.Busy;

            await _unit.Mover.MoveTo(vector3);
            _unit.StateMachine.Enter<UnitSendLightToChurchState>();
        }

        private bool IsLanternReadyToHarvest(Lantern lantern)
        {
            var lanternResource = lantern.GetComponent<LightStorage>();
            return lanternResource.HasAny();
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