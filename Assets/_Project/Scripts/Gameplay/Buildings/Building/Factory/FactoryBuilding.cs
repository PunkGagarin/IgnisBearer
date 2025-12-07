using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class FactoryBuilding : Building
    {
        public double CurrentResourceCount { get; }

        public void Init()
        {
            StartGenerating();
        }

        public bool CanCollect()
        {
            throw new NotImplementedException();
        }

        public void StartGenerating()
        {
            Debug.Log("FactoryBuilding StartGenerating");
            // todo
        }

        public void StopGenerating()
        {
            throw new NotImplementedException();
        }
    }
}