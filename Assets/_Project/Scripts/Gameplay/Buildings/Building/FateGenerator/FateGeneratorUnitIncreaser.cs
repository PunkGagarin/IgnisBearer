using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    [RequireComponent(typeof(IWorkers))]
    public class FateGeneratorUnitIncreaser : BuildingUnitIncreaser
    {
        protected override void OnUnitAdded(Unit unit)
        {
            unit.StateMachine.Enter<UnitMoveToWithNext, DisableState, Vector3>(transform.position);
        }

    }
}