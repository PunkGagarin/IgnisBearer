using UnityEngine;

namespace _Project.Scripts.Gameplay.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Gameplay/LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField]
        public LevelInfo LevelInfo { get; set; }
    }
}