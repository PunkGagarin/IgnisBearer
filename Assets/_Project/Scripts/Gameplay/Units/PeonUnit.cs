using _Project.Scripts.Gameplay.Units.Machine;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    [RequireComponent(typeof(UnitMover))]
    public class PeonUnit : MonoBehaviour
    {
        
        public UnitStateMachine StateMachine { get; private set; }
        public UnitContext Context { get; private set; }
        public UnitMover Mover { get; private set; }

        public void Construct(UnitStateMachine stateMachine, UnitContext context)
        {
            StateMachine = stateMachine;
            Context = context;
            Mover = GetComponent<UnitMover>();
            StateMachine.Enter<UnitIdleState>();
        }

        private void Awake()
        {
        }

        private void Update()
        {
        }

        //idle
        //moveToLantern
        //moveToChurch
        //moveToHome
        //FirUpLantern
        //HarvestLight
        //GiveResourceToChurch
    }
}