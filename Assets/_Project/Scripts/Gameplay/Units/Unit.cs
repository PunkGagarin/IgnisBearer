using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    [RequireComponent(typeof(UnitMover))]
    public class Unit : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject Visual { get; private set; }
        
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

        private void Update()
        {
            StateMachine.Update();
        }

        public void SetVisualStatus(bool isActive)
        {
            Visual.SetActive(isActive);
        }
    }
}