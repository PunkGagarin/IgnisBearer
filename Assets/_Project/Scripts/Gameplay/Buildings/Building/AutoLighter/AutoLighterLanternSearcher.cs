using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoLighterLanternSearcher : MonoBehaviour
    {

        [Inject] private readonly LanternService _lanternService;

        private Workers _workers;
        private Queue<Lantern> _neededToFireUpLanterns = new();

        private void Awake()
        {
            _workers = GetComponent<Workers>();
        }

        private void Start()
        {
            _workers.OnUnitAdded += SubscribeUnit;
            _workers.OnUnitRemoved += UnsubscribeUnit;
            _lanternService.OnLanternNeededToFire += SendFirstUnitToFireUp;

            FindUnfiredLanterns();
        }

        private void FindUnfiredLanterns()
        {
            var unfiredLanterns = _lanternService.GetUnfiredLanterns();
            foreach (var lantern in unfiredLanterns)
                AddLanternToQueue(lantern);
        }

        private void AddLanternToQueue(Lantern lantern)
        {
            _neededToFireUpLanterns.Enqueue(lantern);
        }

        private void OnDestroy()
        {
            foreach (var unit in _workers.CurWorkers)
                UnsubscribeUnit(unit);

            _lanternService.OnLanternNeededToFire -= SendFirstUnitToFireUp;
        }

        private void SubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree += CheckLanternInQueue;
        }

        private void UnsubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree -= CheckLanternInQueue;
        }

        private void CheckLanternInQueue(Unit unit)
        {
            if (_neededToFireUpLanterns.Count > 0)
            {
                var lantern = _neededToFireUpLanterns.Dequeue();
                unit.StateMachine.Enter<UnitMoveToLanternState, Lantern>(lantern);
            }
        }


        private void SendFirstUnitToFireUp(Lantern lantern)
        {
            if (!_workers.HasAnyFreeWorker(out var unit))
            {
                AddLanternToQueue(lantern);
                return;
            }

            unit.StateMachine.Enter<UnitMoveToLanternState, Lantern>(lantern);
        }

        private bool NoUnitAvailable()
        {
            _workers.HasAnyWorker();
            return true;
        }


        // private void Start()

        // {

        //     _lanternService.OnLanternFull += SendFirstUnitToLantern;

        // }

        //

        // private void SendFirstUnitToLantern(Lantern lantern)

        // {

        //     Debug.LogError("trying to send first free unit to harvest light!");

        //     if (!_workers.HasAnyFreeWorker(out var unit))

        //         return;

        //

        //     unit.StateMachine.Enter<UnitMoveToLanternState, Lantern>(lantern);

        // }

    }
}