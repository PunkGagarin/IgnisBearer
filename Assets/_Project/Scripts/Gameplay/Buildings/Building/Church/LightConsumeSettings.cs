using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    // [CreateAssetMenu(fileName = "LightConsumeSettings", menuName = "LightConsumeSettings", order = 0)]
    public class LightConsumeSettings : ScriptableObject
    {
        [field: SerializeField]
        private List<LightConsumeProgress> ConsumeProgress { get; set; } = new()
        {
            new LightConsumeProgress { TimeToIncrease = 0, Amount = 1 }
        };

        public LightConsumeProgress GetProgressByIndex(int index)
        {
            if (index >= ConsumeProgress.Count)
            {
                Debug.LogError(" Index out of range");
                return ConsumeProgress[ConsumeProgress.Count - 1];
            }
            return ConsumeProgress[index];
        }


        private void OnValidate()
        {
            for (int i = ConsumeProgress.Count - 1; i >= 1; i--)
            {
                var consumeProgress = ConsumeProgress[i];
                var timeToIncrease = consumeProgress.TimeToIncrease;
                if (timeToIncrease < ConsumeProgress[i - 1].TimeToIncrease)
                {
                    Debug.LogError(
                        "АЛЛО ДИЗАЙНЕР, СМОТРИ ЧЁ СТАВИШЬ!! время в TimeToIncrease не может быть меньше предыдущего!!");
                }
            }
        }
    }

    [Serializable]
    public class LightConsumeProgress
    {
        [field: SerializeField]
        public float TimeToIncrease { get; set; }

        [field: SerializeField]
        public float TimeToConsume { get; set; }

        [field: SerializeField]
        public int Amount { get; set; }
    }
}