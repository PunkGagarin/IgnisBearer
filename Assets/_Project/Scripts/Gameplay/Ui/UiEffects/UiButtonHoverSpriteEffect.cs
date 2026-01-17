using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(Button))]
    public class UiButtonHoverSpriteEffect : UiButtonHover
    {
        [field: SerializeField] private Sprite _deafultImage;
        [field: SerializeField] private Sprite _hoverImage;

        protected override void ScaleUp()
        {
            base.ScaleUp();
            _button.image.sprite = _hoverImage;
        }

        protected override void ScaleDown()
        {
            base.ScaleDown();
            _button.image.sprite = _deafultImage;
        }
    }
}