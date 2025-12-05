using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternClickDetector : MonoBehaviour
    {

        [field: SerializeField]
        public Button Button { get; private set; }

        public Action<Lantern> OnClicked = delegate { };

        private Lantern _lantern;

        private void Awake()
        {
            _lantern = GetComponent<Lantern>();
            Button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            OnClicked.Invoke(_lantern);
        }

        public void TurnOnClick()
        {
            Button.gameObject.SetActive(true);
        }

        public void TurnOffClick()
        {
            Button.gameObject.SetActive(false);
        }

    }
}