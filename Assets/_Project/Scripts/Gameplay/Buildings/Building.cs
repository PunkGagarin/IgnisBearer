using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [field: SerializeField] public BuildingType Type { get; private set; }
        public string SlotId { get; private set; }

        public void AttachToSlot(string slotId) => SlotId = slotId;
        
        protected virtual void Awake()
        {
        }
    }
}