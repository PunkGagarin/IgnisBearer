using System;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    public class AddRemoveWithCountRow : ContentUi
    {
        public event Action OnAddClicked = delegate { };
        public event Action OnRemoveClicked = delegate { };
        
        [SerializeField] private Button _addUnitButton;
        [SerializeField] private Button _removeUnitButton;
        [SerializeField] private TextMeshProUGUI _countText;
        
        private void Awake()
        {
            _addUnitButton.onClick.AddListener(OnAddButtonClicked);
            _removeUnitButton.onClick.AddListener(OnRemoveButtonClicked);
        }
        
        private void OnDestroy()
        {
            _addUnitButton.onClick.RemoveListener(OnAddButtonClicked);
            _removeUnitButton.onClick.RemoveListener(OnRemoveButtonClicked);
        }
        
        private void OnRemoveButtonClicked() => OnRemoveClicked?.Invoke();

        private void OnAddButtonClicked() => OnAddClicked.Invoke();

        public void UpdateUi(string countText, bool isAddEnabled, bool isRemoveEnabled)
        {
            _countText.text = countText;
            _addUnitButton.interactable = isAddEnabled;
            _removeUnitButton.interactable = isRemoveEnabled;
        }
    }
}
