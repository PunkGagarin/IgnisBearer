using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public class HarvestResourceState : IUnitState, IPayloadState<LightResource>
    {
        // [Inject] private LanternSettings _lanternSettings;

        private Unit _unit;
        // private float _currentTime = 0f;
        // private ResourceStorage _resourceStorage;
        // private LanternUi _lanternUi;
        private UnitContext Context => _unit.Context;

        // private Action _enterNextState;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Enter(LightResource resource)
        {
            resource.Harvest();
            // _resourceStorage = lantern.GetComponent<ResourceStorage>();
            // _lanternUi = lantern.GetComponent<LanternUi>();
            // _resourceStorage.StartCollecting();
        }

        public void Update()
        {
            // _currentTime += Time.deltaTime * _unit.Context.FireUpMultiplier;
            //
            // UpdateBar(_currentTime, _lanternSettings.HarvestTime);
            //
            // if (_currentTime > _lanternSettings.HarvestTime)
            // {
            //     Context.LightAmount = _resourceStorage.Collect();
            //     _unit.StateMachine.Enter<UnitMoveToChurchState>();
            // }
        }

        public void Exit()
        {
            // _currentTime = 0f;
            // _resourceStorage = null;
            // _lanternUi = null;
        }

        // private void UpdateBar(float currentTime, float lanternSettingsHarvestTime)
        // {
        //     _lanternUi.SetProgress(currentTime / lanternSettingsHarvestTime);
        // }
    }
}