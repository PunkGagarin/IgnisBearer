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

        protected override void SubscribeToSearch()
        {
            _lanternService.OnLanternNeededToFire += SendFirstUnit;
            _lanternService.OnLanternFired += RemoveLantern;
            _lanternService.OnLanternCreated += AddLantern;
        }

        protected override void UnsubscribeFromRes()
        {
            _lanternService.OnLanternNeededToFire -= SendFirstUnit;
            _lanternService.OnLanternFired -= RemoveLantern;
            _lanternService.OnLanternCreated -= AddLantern;
        }

        private void AddLantern(Lantern obj)
        {
            FindResForQueue();
            if (_resToServe != null)
            {
                foreach (var lantern in _resToServe)
                {
                    SendFirstUnit(lantern);
                }
            }
        }

        private void RemoveLantern(Lantern lantern)
        {
            if(_resToServe.Contains(lantern))
                _resToServe.Remove(lantern);
        }

        protected override void MoveTo(Unit unit, Lantern res)
        {
            unit.StateMachine.Enter<UnitMoveToWithNextAndPayload, FireUpLanternState, Vector3, Lantern>(
                res.transform.position, res);
        }
    }
}