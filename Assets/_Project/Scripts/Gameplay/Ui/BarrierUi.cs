using System;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui
{
    public class BarrierUi : ContentUi
    {

        [field: SerializeField]
        public BarUi Bar { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI BarrierCounter { get; private set; }

        private void Awake()
        {
            Show();
        }

        public void SetBarrierCounter(int current, int max)
        {
            BarrierCounter.text = $"{current}/{max}";
        }
    }
}