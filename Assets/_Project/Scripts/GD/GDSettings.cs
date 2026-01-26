using UnityEngine;

namespace _Project.Scripts.GD
{
    // [CreateAssetMenu(fileName = "GDSettings", menuName = "GDSettings", order = 0)]
    public class GDSettings : ScriptableObject
    {
        
        [field: SerializeField]
        public bool IsConsumeStartedByDefault { get; private set; }

        [field: SerializeField]
        public bool IsTutorialOn { get; private set; } = true;
        
        [field: SerializeField]
        public bool ClearPrefsOnStart { get; private set; } = true;
    }
}