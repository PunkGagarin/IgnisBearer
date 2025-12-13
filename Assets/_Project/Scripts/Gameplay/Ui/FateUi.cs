using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui
{
    public class FateUi : ContentUi
    {
        [field: SerializeField]
        public TextMeshProUGUI FateCounter { get; private set; }

        public void SetFateCounter(int current)
        {
            FateCounter.text = $"{current}";
        }
    }
}