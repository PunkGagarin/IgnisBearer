using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui
{
    public class CloseContentUiByButton : MonoBehaviour
    {
        [field: SerializeField]
        public Button СloseButton { get; private set; }

        private void Awake()
        {
            СloseButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            СloseButton.onClick.RemoveListener(Hide);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}