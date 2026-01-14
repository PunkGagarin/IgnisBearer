using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "BuildingSettings", menuName = "Gameplay/Buildings/BuildingSettings", order = 0)]
    public class BuildingSettings : ScriptableObject
    {
        [field: SerializeField]
        public List<BuildingType> AvailableToBuildBuildings { get; private set; }

        [field: SerializeField]
        public List<BuildingType> PrebuildBuildings { get; private set; }
    }
}