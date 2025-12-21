using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoHarvestSearcher : BaseSearcher<LightResource>
    {
        [Inject] protected readonly LightResourceService _resourceService;

        protected override void SubscribeToSearch()
        {
            _resourceService.OnLightCreated += SendFirstUnit;
        }

        protected override List<LightResource> GetResForQueue()
        {
            return _resourceService.GetUnharvestedResources();
        }

        protected override void UnsubscribeFromRes()
        {
            _resourceService.OnLightCreated -= SendFirstUnit;
        }

        protected override void MoveTo(Unit unit, LightResource res)
        {
            unit.StateMachine.Enter<MoveToWithNextAndPayload, HarvestResourceState, Vector3, LightResource>(
                res.transform.position, res);
        }
    }
}