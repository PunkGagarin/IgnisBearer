using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [field: SerializeField] public BuildingType Type { get; private set; }
        public event Action OnDestroyed = delegate { };

        protected virtual void Awake()
        {
        }
        protected virtual void OnDestroy() => OnDestroyed?.Invoke();
    }
}