using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitStateMachine : SimpleStateMachine<IUnitState>
    {
        
        public void Update()
        {
            _currentState.Update();
        }

    }
}