using _Project.Scripts.Gameplay.Ui.UiEffects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    public class OpenBuildingPopup : MonoBehaviour
    {

        [SerializeField]
        private Button _button;

        [FormerlySerializedAs("_popup")] [SerializeField]
        private UiPopupDisplayer popupDisplayer;

        protected virtual void Awake()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDestroy()
            => _button.onClick.RemoveListener(HandleButtonClick);

        protected virtual void HandleButtonClick()
        {
            popupDisplayer.AnimateAndShow();
        }

        public void SetButtonEnabled(bool isEnabled) => _button.gameObject.SetActive(isEnabled);

    }
}