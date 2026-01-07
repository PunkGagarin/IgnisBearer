using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Tutorial
{
    public class TutorialUi : ContentUi
    {
        [field: SerializeField]
        public TextMeshProUGUI Text { get; private set; }

        public void SetTutorialText(string text)
        {
            Text.text = text;
            Show();
        }
    }
}