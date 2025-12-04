using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "AutoCollectorSettings", menuName = "Gameplay/Buildings/AutoCollectorSettings", order = 0)]
    public class AutoCollectorSettings: ScriptableObject
    {
        [field: SerializeField] public double BuildPrice { get; private set; }
    }
}