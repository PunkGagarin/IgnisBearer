using System;
using System.Globalization;
using _Project.Scripts.Localization;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Tooltips
{
    public class TooltipUi : ContentUi
    {
        [Inject] private TooltipController _tooltipController;
        
        [field: SerializeField] private TooltipUiData _uiData;
        [Header("Fields")]
        [field: SerializeField] private ToLocalize _titleToLocalize;
        [field: SerializeField] private ToLocalize _levelDescToLocalize;
        [field: SerializeField] private TextMeshProUGUI _level;
        [field: SerializeField] private ToLocalize _descToLocalize;
        [field: SerializeField] private TextMeshProUGUI _priceCount;

        public RectTransform Target { get; private set; }
        
        public event Action OnOpened = delegate { };

        public void SetData(TooltipUiData uiData, RectTransform target)
        {
            Target = target;
            _titleToLocalize.SetKey(uiData.TitleKey);
            _levelDescToLocalize.SetKey(uiData.LevelDescKey);
            _level.text = uiData.Level.ToString();
            _descToLocalize.SetKey(uiData.DescriptionKey);
            _priceCount.text = uiData.Price.ToString(CultureInfo.InvariantCulture);
        }

        public void AnimateAndShow()
        {
            _tooltipController.Register(this);
            Show();
            OnOpened?.Invoke();
        }

        public void AnimateAndHide()
        {
            Hide();
        }
    }
}