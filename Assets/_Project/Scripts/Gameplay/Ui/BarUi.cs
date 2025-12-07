using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui
{
    public class BarUi : MonoBehaviour
    {
        [field: SerializeField]
        public Image Bar { get; private set; }

        public void ChangeBarProgress(float progress)
        {
            Bar.fillAmount = progress;
        }

        public void TurnOnBar()
        {
            gameObject.SetActive(true);
        }

        public void TurnOffBar()
        {
            gameObject.SetActive(false);
        }
    }
}