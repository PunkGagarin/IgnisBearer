using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.BuildingsSlots
{
    public class BuildingSlot : MonoBehaviour
    {
        public event Action<BuildingSlot> OnClicked;

        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private void Awake() => _button.onClick.AddListener(OnClick);

        private void OnDestroy() => _button.onClick.RemoveListener(OnClick);

        private void OnClick() => OnClicked?.Invoke(this);

        public void SetEnabled(bool enabled) => _button.gameObject.SetActive(enabled);

        public void SetSprite(Sprite sprite) => _image.sprite = sprite;
    }
}