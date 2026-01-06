using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Gameplay.Ui.Utils
{
    public class UiPan : MonoBehaviour, IDragHandler
    {
        public RectTransform treeContainer;

        public void OnDrag(PointerEventData eventData)
        {
            treeContainer.anchoredPosition += eventData.delta;
        }
    }
}