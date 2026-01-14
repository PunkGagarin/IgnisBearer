using _Project.Scripts.Gameplay.Ui.UiEffects;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(UiPopupDisplayer))]
    public class PopupClampToScreen : MonoBehaviour
    {
        [Inject] private readonly Camera _uiCamera;

        [field: SerializeField] private RectTransform _rectTransform;
        private UiPopupDisplayer _uiPopupDisplayer;

        private void Awake()
        {
            _uiPopupDisplayer = GetComponent<UiPopupDisplayer>();
            _uiPopupDisplayer.OnOpened += ClampToScreen;
        }

        private void OnDestroy()
        {
            _uiPopupDisplayer.OnOpened -= ClampToScreen;
        }

        private void ClampToScreen()
        {
            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);

            Vector3 offset = Vector3.zero;

            var bottomLeftPopupCoord = _uiCamera.WorldToScreenPoint(corners[0]);
            var topRightPopupCoord = _uiCamera.WorldToScreenPoint(corners[2]);

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            offset = CheckLeft(bottomLeftPopupCoord, offset);

            offset = CheckRight(topRightPopupCoord, screenWidth, offset);

            offset = CheckBottom(bottomLeftPopupCoord, offset);

            offset = CheckTop(topRightPopupCoord, screenHeight, offset);

            var worldOffset = GetWorldOffset(offset);

            _rectTransform.position += worldOffset;
        }

        private Vector3 GetWorldOffset(Vector3 offset)
        {
            var worldOffset =
                _uiCamera.ScreenToWorldPoint(
                    new Vector3(offset.x, offset.y, _uiCamera.WorldToScreenPoint(_rectTransform.position).z)
                ) - _uiCamera.ScreenToWorldPoint(
                    new Vector3(0, 0, _uiCamera.WorldToScreenPoint(_rectTransform.position).z)
                );
            return worldOffset;
        }

        private static Vector3 CheckTop(Vector3 topRightUiCameraCoord, float screenHeight, Vector3 offset)
        {
            if (topRightUiCameraCoord.y > screenHeight)
                offset.y -= topRightUiCameraCoord.y - screenHeight;
            return offset;
        }

        private static Vector3 CheckBottom(Vector3 bottomLeftUiCameraCoord, Vector3 offset)
        {
            if (bottomLeftUiCameraCoord.y < 0)
                offset.y += -bottomLeftUiCameraCoord.y;
            return offset;
        }

        private static Vector3 CheckRight(Vector3 topRightUiCameraCoord, float screenWidth, Vector3 offset)
        {
            if (topRightUiCameraCoord.x > screenWidth)
                offset.x -= topRightUiCameraCoord.x - screenWidth;
            return offset;
        }

        private static Vector3 CheckLeft(Vector3 bottomLeftUiCameraCoord, Vector3 offset)
        {
            if (bottomLeftUiCameraCoord.x < 0)
                offset.x += -bottomLeftUiCameraCoord.x;
            return offset;
        }
    }
}