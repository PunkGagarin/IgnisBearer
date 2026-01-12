using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(Button))]
    public class UiButtonHover : UiHover
    {
        private Button _button;

        protected override void Awake()
        {
            base.Awake();
            _button = GetComponent<Button>();
        }

        protected override bool CanScale()
        {
            return _button.interactable;
        }
    }
}