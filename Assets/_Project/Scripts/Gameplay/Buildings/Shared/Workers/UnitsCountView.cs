using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class UnitsCountView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Init(int curCount, int maxCount)
        {
            _text.text = $"{curCount}/{maxCount}";
        }
    }
}