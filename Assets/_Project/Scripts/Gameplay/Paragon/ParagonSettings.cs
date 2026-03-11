using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    // [CreateAssetMenu(fileName = "ParagonSettings", menuName = "ParagonSettings", order = 0)]
    public class ParagonSettings : ScriptableObject
    {

        [field: SerializeField]
        public List<ParagonTimerSettings> TimeSettings { get; set; } = new();

        [field: SerializeField]
        public int TimeToWin { get; set; } = 600;

        private void OnValidate()
        {
            if (TimeSettings.Count == 0)
                Debug.LogError("Paragon Time settings = 0!! Set something!");
        }


        public ParagonTimerSettings GetParagonTimeSettings(int currentParagonGoalIndex)
        {
            if (currentParagonGoalIndex >= TimeSettings.Count)
            {
                Debug.LogError($"Trying to get Paragon Time settings out of range: " +
                               $"{currentParagonGoalIndex}, count: {TimeSettings.Count}");
                currentParagonGoalIndex = TimeSettings.Count - 1;
            }

            return TimeSettings[currentParagonGoalIndex];
        }
    }
}