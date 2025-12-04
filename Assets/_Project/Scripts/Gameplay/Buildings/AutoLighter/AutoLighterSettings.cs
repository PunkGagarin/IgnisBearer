using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "AutoLighterSettings", menuName = "Gameplay/Buildings/AutoLighterSettings", order = 0)]
    public class AutoLighterSettings: ScriptableObject
    {
        [field: SerializeField] public double BuildPrice { get; private set; }
    }
}