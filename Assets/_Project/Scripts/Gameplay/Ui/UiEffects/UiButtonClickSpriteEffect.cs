using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(Button), typeof(UiShake))]
    public class UiButtonClickSpriteEffect : UiButtonClickEffect
    {
        [field: SerializeField] private Sprite _deafultImage;
        [field: SerializeField] private Sprite _hoverImage;

        protected override void ClickDown()
        {
            base.ClickDown();
            _button.image.sprite = _hoverImage;
        }

        protected override void ClickUp()
        {
            base.ClickUp();
            _button.image.sprite = _deafultImage;
        }
    }
}