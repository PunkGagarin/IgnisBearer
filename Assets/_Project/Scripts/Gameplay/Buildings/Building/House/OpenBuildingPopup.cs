using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class OpenBuildingPopup : MonoBehaviour
    {

        [SerializeField]
        private Button _button;

        [SerializeField]
        private GameObject _popup;


        protected virtual void Awake()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDestroy()
            => _button.onClick.RemoveListener(HandleButtonClick);

        protected virtual void HandleButtonClick()
        {
            _popup.SetActive(true);
        }
    }
}