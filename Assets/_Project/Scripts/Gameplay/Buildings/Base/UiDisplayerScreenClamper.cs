using _Project.Scripts.Gameplay.Ui.UiEffects;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(UiPopupDisplayer), typeof(WorldScreenClamper))]
    public class UiDisplayerScreenClamper : WorldScreenClamper
    {
        [Inject] private readonly Camera _uiCamera;
        private WorldScreenClamper _worldScreenClamper;
        private UiPopupDisplayer _uiPopupDisplayer;

        private void Awake()
        {
            _worldScreenClamper = GetComponent<WorldScreenClamper>();
            _uiPopupDisplayer = GetComponent<UiPopupDisplayer>();
            _uiPopupDisplayer.OnOpened += _worldScreenClamper.ClampToScreen;
        }

        private void OnDestroy()
        {
            _uiPopupDisplayer.OnOpened -= _worldScreenClamper.ClampToScreen;
        }
    }
}