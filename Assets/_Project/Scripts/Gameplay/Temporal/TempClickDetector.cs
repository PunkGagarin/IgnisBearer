using System;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Temporal
{
    public class TempClickDetector : MonoBehaviour
    {
        
        [field: SerializeField]
        public Button Button { get; private set; }

        public Action<TemporalLantern> OnClicked = delegate { };
        private TemporalLantern _temporalLantern;

        private void Awake()
        {
            _temporalLantern = GetComponent<TemporalLantern>();
            Button.onClick.AddListener(OnButtonClicked);
        }
        
        private void OnButtonClicked()
        {
            OnClicked.Invoke(_temporalLantern);
        }

    }
}