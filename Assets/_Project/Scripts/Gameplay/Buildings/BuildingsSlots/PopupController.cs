using _Project.Scripts.Gameplay.Ui.UiEffects;

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
            _uiPopupDisplayer = uiPopupDisplayer;
        }
    }
}