using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoLighterSearcher : BaseSearcher<Lantern>
    {
        [Inject] protected readonly LanternService _lanternService;
        
        protected override List<Lantern> GetResForQueue()
        {
            return _lanternService.GetUnfiredLanterns();
        }

        protected override void UnsubscribeFromRes()
        {
            _lanternService.OnLanternNeededToFire -= SendFirstUnit;
        }

        protected override void MoveTo(Unit unit, Lantern res)
        {
            unit.StateMachine.Enter<MoveToWithNextAndPayload, FireUpLanternState, Vector3, Lantern>(
                res.transform.position, res);
        }
        
        protected override void SubscribeToSearch()
        {
            _lanternService.OnLanternNeededToFire += SendFirstUnit;
        }
    }
}