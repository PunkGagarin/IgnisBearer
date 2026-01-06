using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui.Utils
{
    public class UiZoom : MonoBehaviour
    {
        public RectTransform treeContainer;
        
        public float zoomSpeed = 0.1f;
        public float minZoom = 0.5f;
        public float maxZoom = 2f;

        void Update()
        {
            float scroll = Input.mouseScrollDelta.y;
            if (scroll == 0) return;

            float scale = treeContainer.localScale.x;
            scale += scroll * zoomSpeed;
            scale = Mathf.Clamp(scale, minZoom, maxZoom);

            treeContainer.localScale = Vector3.one * scale;
        }
    }
}