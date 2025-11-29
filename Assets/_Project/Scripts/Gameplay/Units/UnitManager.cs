using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitManager : MonoBehaviour
    {
        [Inject] private UnitFactory _factory;


        private void Start()
        {
            _factory.CreateAndInstantiateUnit();
        }
    }
}