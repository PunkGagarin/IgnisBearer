using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Gameplay.Ui.Tooltips;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillNodeUI : MonoBehaviour
    {
        [Inject] 
        private readonly SkillTreeSettings _skillTreeSettings;
        
        [field: SerializeField]
        private Button MainButton { get; set; }

        [field: SerializeField]
        public SkillNodeType Type { get; private set; }

        [field: SerializeField]
        private Image Icon { get; set; }

        [field: SerializeField]
        private Image Background { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI Price { get; set; }

        [field: SerializeField]
        private Image PriceIcon { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI CurrentLevelText { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI MaxLevelText { get; set; }

        [field: SerializeField]
        private List<GameObject> ArrowsFromHere { get; set; } = new();

        [field: SerializeField]
        public List<SkillNodeUI> NextNodes { get; private set; }

        [field: SerializeField]
        private SkillNodeUI ParentNode { get; set; }

        private SkillNodeState State { get; set; }
        
        public RectTransform RectTransform { get; private set; }
        public TooltipUiData TooltipUiData { get; private set; }
        
        public event Action<SkillNodeUI> OnClick = delegate { };

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            MainButton.onClick.AddListener(OnClickHandle);
        }

        private void OnDestroy()
        {
            MainButton.onClick.RemoveListener(OnClickHandle);
        }

        private void OnClickHandle()
        {
            OnClick.Invoke(this);
        }

        public void SetState(NodeBoughtState boughtState)
        {
            switch (boughtState)
            {
                case NodeBoughtState.Maxed:
                    SetMaxed();
                    break;
                case NodeBoughtState.Bought:
                    SetNodeActive();
                    break;
                case NodeBoughtState.NotBought:
                    ActivateSelf();
                    break;
                case NodeBoughtState.None:
                    break;
            }
        }
        
        public void SetState(SkillNodeState state)
        {
            switch (state)
            {
                case SkillNodeState.CanBuy:
                    SetCanBuy();
                    break;
                case SkillNodeState.NoMoney:
                    SetNoMoney();
                    break;
                case SkillNodeState.None:
                case SkillNodeState.Unreachable:
                default:
                    break;
            }
        }

        private void SetMaxed()
        {
            Background.color = _skillTreeSettings.MaxedNodeColor;
            ActivateNode();
            HidePrice();
            Debug.Log(" Включаем состояние ноды - замакшена");
        }

        private void ActivateSelf()
        {
            gameObject.SetActive(true);
        }

        public void SetNodeActive()
        {
            Background.color = _skillTreeSettings.CanBuyNodeColor;
            ActivateNode();
        }

        private void ActivateNode()
        {
            ActivateArrows();
            ActivateNextNodes();
        }

        private void ActivateArrows()
        {
            foreach (var arrow in ArrowsFromHere)
                arrow.gameObject.SetActive(true);
        }

        private void ActivateNextNodes()
        {
            foreach (var skillNodeUI in NextNodes)
                skillNodeUI.SetState(NodeBoughtState.NotBought);
        }

        private void SetNoMoney()
        {
            Background.color = _skillTreeSettings.NoMoneyNodeColor;
        }

        private void SetCanBuy()
        {
            Background.color = _skillTreeSettings.CanBuyNodeColor;
        }

        public void SetNewLevel(int currentLevel)
        {
            CurrentLevelText.text = currentLevel.ToString();
        }

        public void StartCantBuyAnimation()
        {
            Debug.Log(" Здесь должна быть анимация тряски");
        }

        public void SetMaxLevel(int maxLevel)
        {
            MaxLevelText.text = maxLevel.ToString();
        }

        public void SetPrice(int price)
        {
            Price.text = price.ToString();
        }

        public void SetCurrencyType(MetaCurrencyType currencyType)
        {
            //todo: implement me
            // PriceIcon.sprite = priceIcon;
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }
        
        public void SetTooltipUiData(TooltipUiData tooltipUiData)
        {
            TooltipUiData = tooltipUiData;
        }

        public void HidePrice()
        {
            Price.gameObject.SetActive(false);
            PriceIcon.gameObject.SetActive(false);
        }
    }
}