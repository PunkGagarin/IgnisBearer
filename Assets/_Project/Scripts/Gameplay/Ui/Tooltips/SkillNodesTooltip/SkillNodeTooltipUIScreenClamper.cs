using _Project.Scripts.Gameplay.Ui.Utils;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Tooltips.SkillNodesTooltip
{
    [RequireComponent(typeof(TooltipUi))]
    public class SkillNodeTooltipUIScreenClamper : MonoBehaviour
    {
        [Inject] private readonly Camera _uiCamera;
        [Inject] private readonly GameplayUiRoot _gameplayUiRoot;
        
        [SerializeField] private Vector2 _padding = new(15, 15);
        [SerializeField] private UiZoom _skillTreeZoomer;
        
        private RectTransform _tooltipRect;
        private RectTransform _canvasRect;
        private TooltipUi _tooltipUI;
        
        private void Awake()
        {
            _canvasRect = _gameplayUiRoot?.GetComponent<RectTransform>();
            _tooltipUI = GetComponent<TooltipUi>();
            _tooltipRect = _tooltipUI.GetComponent<RectTransform>();

            _tooltipUI.OnOpened += ClampToScreen;
            _skillTreeZoomer.OnZoomChanged += OnMapScaled;
        }

        private void OnDestroy()
        {
            _tooltipUI.OnOpened -= ClampToScreen;
            _skillTreeZoomer.OnZoomChanged -= OnMapScaled;
        }

        private void OnMapScaled(float scale)
        {
            if (_tooltipUI == null || !_tooltipUI.gameObject.activeSelf) return;
            ClampToScreen();
        }
        
        private void ClampToScreen()
        {
            var target = _tooltipUI.Target;

            if (target == null)
                return;

            //    Canvas.ForceUpdateCanvases();

            PlaceTooltip(target);
        }
        private void PlaceTooltip(RectTransform target)
        {
            Vector3[] corners = new Vector3[4];
            target.GetWorldCorners(corners);

            Vector2 tooltipSize = _tooltipRect.rect.size;
            Vector2 pivot = _tooltipRect.pivot;

            Vector2 rightPos = WorldToCanvas(new Vector2(corners[2].x, (corners[0].y + corners[2].y)/2)) 
                               + new Vector2(tooltipSize.x * pivot.x + _padding.x, 0);
    
            Vector2 leftPos = WorldToCanvas(new Vector2(corners[0].x, (corners[0].y + corners[2].y)/2)) 
                              - new Vector2(tooltipSize.x * (1 - pivot.x) + _padding.x, 0);

            Vector2 topPos = WorldToCanvas(new Vector2((corners[0].x + corners[2].x)/2, corners[2].y)) 
                             + new Vector2(0, tooltipSize.y * (1 - pivot.y) + _padding.y);

            Vector2 bottomPos = WorldToCanvas(new Vector2((corners[0].x + corners[2].x)/2, corners[0].y)) 
                                - new Vector2(0, tooltipSize.y * pivot.y + _padding.y);

            if (Fits(rightPos, tooltipSize))
                _tooltipRect.anchoredPosition = rightPos;
            else if (Fits(leftPos, tooltipSize))
                _tooltipRect.anchoredPosition = leftPos;
            else if (Fits(topPos, tooltipSize))
                _tooltipRect.anchoredPosition = topPos;
            else
                _tooltipRect.anchoredPosition = bottomPos;
        }

        private Vector2 WorldToCanvas(Vector3 worldPos)
        {
            return _canvasRect.InverseTransformPoint(worldPos);
        }

        private bool Fits(Vector2 pos, Vector2 size)
        {
            Rect rect = new Rect(pos, size);

            return rect.xMin >= _canvasRect.rect.xMin &&
                   rect.xMax <= _canvasRect.rect.xMax &&
                   rect.yMin >= _canvasRect.rect.yMin &&
                   rect.yMax <= _canvasRect.rect.yMax;
        }

    }
}