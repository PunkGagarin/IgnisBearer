using _Project.Scripts.Gameplay.Ui.UiEffects;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class PopupController
    {
        private UiPopupDisplayer _uiPopupDisplayer;

        public void Register(UiPopupDisplayer uiPopupDisplayer)
        {
            _uiPopupDisplayer?.AnimateAndHide();
            _uiPopupDisplayer = uiPopupDisplayer;
        }
    }
}