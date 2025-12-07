using System;

namespace _Project.Scripts.Gameplay.Buildings.BuildingComponents
{
    public interface IChurchLightStorage
    {
        int Count { get; }
        int MaxCount { get; }
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
    }
}