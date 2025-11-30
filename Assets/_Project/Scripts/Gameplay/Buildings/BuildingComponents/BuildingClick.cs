using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Gameplay.BuildingComponents
{
    [RequireComponent(typeof(Collider2D))]
    public class BuildingClick : MonoBehaviour
    {
        public event Action OnClicked = delegate { };

        private void OnMouseDown()
        {
            if (IsPointerOverUI()) return;

            OnClicked.Invoke();
        }

        private bool IsPointerOverUI() =>
            EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}