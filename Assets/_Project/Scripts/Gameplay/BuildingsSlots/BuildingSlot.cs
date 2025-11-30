using UnityEngine;

namespace _Project.Scripts.Gameplay.BuildingsSlots
{
    public class BuildingSlot : ClickableView<BuildingSlot>
    // todo click by btn
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake() =>
            _spriteRenderer = GetComponent<SpriteRenderer>();

        public void SetEnabled(bool enabled) =>
            gameObject.SetActive(enabled);

        public void SetSprite(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;
    }
}