using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchBuilding : Building
    {
        [field: SerializeField]
        public GameObject FateGenerator { get; private set; }
    }
}