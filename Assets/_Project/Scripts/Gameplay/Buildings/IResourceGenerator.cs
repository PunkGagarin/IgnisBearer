
using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IResourceGenerator
    {
        Double CurrentResourceCount { get; }
        bool CanCollect();
        void StartGenerating();
        void StopGenerating();
    }
}