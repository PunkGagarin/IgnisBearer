using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Factory
{
    public class FactoryBuilding : Building, IResourceGenerator
    {
        public double CurrentResourceCount { get; }
        public void Init()
        {
            StartGenerating();
        }

        public bool CanCollect()
        {
            throw new System.NotImplementedException();
        }

        public void StartGenerating()
        {
            Debug.Log("FactoryBuilding StartGenerating");
            // todo
        }

        public void StopGenerating()
        {
            throw new System.NotImplementedException();
        }
    }
}