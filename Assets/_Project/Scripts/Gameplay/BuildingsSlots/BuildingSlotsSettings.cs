using UnityEngine;

namespace _Project.Scripts.Gameplay.BuildingsSlots
{
    // [CreateAssetMenu(fileName = "BuildingSlotsSettings", menuName = "Gameplay/Buildings/BuildingSlotsSettings", order = 0)]

    public class BuildingSlotsSettings : ScriptableObject
    {
        [field: SerializeField] public BuildingSlot Prefab { get; set; }
    }
}