using System;
using _Project.Scripts.Gameplay.Ui;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    [Serializable]
    public class ParagonTimerSettings
    {
        [field: SerializeField]
        public int Second { get; set; }

        [field: SerializeField]
        public MetaCurrencyType CurrencyType { get; set; }

        [field: SerializeField]
        public int Amount { get; set; }
    }
}