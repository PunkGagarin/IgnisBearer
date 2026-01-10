using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class HarvestResourceState : IUnitState, IPayloadState<LightResource>
    {
        [Inject] BuildingsService _buildingsService;
        private Unit _unit;

        private UnitContext Context => _unit.Context;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter(LightResource resource)
        {
            resource.Harvest();

            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            Context.LightAmount += 1;
            
            _buildingsService.GetChurch().GetComponent<ChurchQueue>().SendUnitToQueue(_unit);
            // _unit.StateMachine.Enter<UnitMoveToWithNext, UnitAddToChurchQueueState, Vector3>(
            //     _buildingSlotsService.GetChurchPosition());
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}