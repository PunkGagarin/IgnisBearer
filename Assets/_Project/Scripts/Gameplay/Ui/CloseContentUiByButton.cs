using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui
{
    public class CloseContentUiByButton : MonoBehaviour
    {
        [field: SerializeField]
        public Button СloseButton { get; private set; }

        private ContentUi _contentUi;

        private void Awake()
        {
            _contentUi = GetComponent<ContentUi>();
            СloseButton.onClick.AddListener(_contentUi.Hide);
        }

        private void OnDestroy()
        {
            СloseButton.onClick.RemoveListener(_contentUi.Hide);
        }
    }
}