using UnityEngine;

namespace _Project.Scripts.Gameplay.Tutorial
{
    // [CreateAssetMenu(fileName = "TutorialSettings", menuName = "Gameplay/Tutorial/TutorialSettings", order = 0)]
    public class TutorialSettings : ScriptableObject
    {
        [field: SerializeField] public int LightCountForChurch { get; set; }
    }
}