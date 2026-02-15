using _Project.Scripts.Gameplay.SkillTree;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Gameplay.Ui.Tooltips.SkillNodesTooltip
{
    [RequireComponent(typeof(SkillNodeUI))]
    public class SkillNodeTooltipHoverListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField] private TooltipUi _tooltipUi;
        private SkillNodeUI _skillNodeUi;

        private void Awake()
        {
            _skillNodeUi = GetComponent<SkillNodeUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltipUi.SetData(_skillNodeUi.TooltipUiData, _skillNodeUi.RectTransform);
            _tooltipUi.AnimateAndShow();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltipUi.AnimateAndHide();
        }
    }
}