using Zenject;

namespace _Project.Scripts.Gameplay.Vfx.Light
{
    public class ChurchLightSwitcher : LightSwitcher
    {
        [Inject] private readonly FateService _fateService;

        private void Awake()
        {
            _fateService.OnAmountChanged += SwitchOn;
        }

        private void OnDestroy()
        {
            _fateService.OnAmountChanged -= SwitchOn;
        }

        private void SwitchOn((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            SwitchOn();
        }
    }
}