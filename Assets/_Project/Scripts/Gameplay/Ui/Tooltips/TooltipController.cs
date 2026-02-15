namespace _Project.Scripts.Gameplay.Ui.Tooltips
{
    public class TooltipController
    {
        private TooltipUi _tooltip;

        public void Register(TooltipUi tooltip)
        {
            if (_tooltip == tooltip)
                return;

            _tooltip?.AnimateAndHide();
            _tooltip = tooltip;
        }
    }
}