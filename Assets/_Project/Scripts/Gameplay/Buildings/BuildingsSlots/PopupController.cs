using _Project.Scripts.Gameplay.Ui.UiEffects;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class PopupController
    {
        private UiPopupDisplayer _uiPopupDisplayer;

        public void Register(UiPopupDisplayer uiPopupDisplayer)
        {
            if (_uiPopupDisplayer == uiPopupDisplayer)
                return;

            _uiPopupDisplayer?.AnimateAndHide();
            
            Time.timeScale = 0.2f;
            _uiPopupDisplayer = uiPopupDisplayer;
        }

        public void Close(UiPopupDisplayer uiPopupDisplayer)
        {
            Time.timeScale = 1f;
            _uiPopupDisplayer = null;
        }
    }
}