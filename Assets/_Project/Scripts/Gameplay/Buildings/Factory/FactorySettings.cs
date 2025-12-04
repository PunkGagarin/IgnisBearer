using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Factory
{
    [CreateAssetMenu(fileName = "FactorySettings", menuName = "Gameplay/Buildings/FactorySettings", order = 0)]
    public class FactorySettings: ScriptableObject
    {
        [field: SerializeField] public double BuildPrice { get; private set; }
    }
}