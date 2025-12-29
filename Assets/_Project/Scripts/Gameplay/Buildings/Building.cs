using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [field: SerializeField] public BuildingType Type { get; private set; }


        protected virtual void Awake()
        {
        }
    }
}