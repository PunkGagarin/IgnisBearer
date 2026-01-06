using UnityEngine;

namespace _Project.Scripts.Gameplay.Level
{
    public interface IBuildContainer
    {
        Transform GetSlotsContainer();
        Transform GetBuildingContainer();
    }
}