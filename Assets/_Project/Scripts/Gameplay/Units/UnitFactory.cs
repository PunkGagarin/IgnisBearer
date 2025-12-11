using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UnitSettings _unitSettings;

        public Unit CreateAndInstantiateUnit(Transform unitPosition)
        {
            var unit = _container.InstantiatePrefabForComponent<Unit>(_unitSettings.UnitPrefab,
                unitPosition.transform.position, Quaternion.identity, unitPosition.transform);

            var unitContext = new UnitContext(_unitSettings.DefaultMoveSpeed, _unitSettings.DefaultFireUpSpeed);
            var unitStateMachine = new UnitStateMachine();
            
            var idle = _container.Instantiate<UnitIdleState>();
            idle.Init(unit); 
            
            var moveToLantern = _container.Instantiate<UnitMoveToLanternState>();
            moveToLantern.Init(unit);  
            
            var fireUp = _container.Instantiate<FireUpLanternState>();
            fireUp.Init(unit); 
            
            var harvestLantern = _container.Instantiate<HarvestLanternState>();
            harvestLantern.Init(unit);     
            
            var moveToChurch = _container.Instantiate<UnitMoveToChurchState>();
            moveToChurch.Init(unit); 
            
            var sendToChurch = _container.Instantiate<UnitSendLightToChurchState>();
            sendToChurch.Init(unit);
            
            unitStateMachine.Register(idle);
            unitStateMachine.Register(moveToLantern);
            unitStateMachine.Register(fireUp);
            unitStateMachine.Register(harvestLantern);
            unitStateMachine.Register(moveToChurch);
            unitStateMachine.Register(sendToChurch);

            unit.Construct(unitStateMachine, unitContext);
            
            return unit;
        }
    }

}