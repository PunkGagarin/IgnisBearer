using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillNodeUI : MonoBehaviour
    {

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

        //todo: move to settinsg and get from there
        private Color ActiveNodeColor { get; set; } = Color.yellow;

        private Color NodeUnreachableColor { get; set; } = Color.gray;

        private Color CanBuyNodeColor { get; set; } = Color.white;

        private Color NoMoneyNodeColor { get; set; } = Color.red;


        private SkillNodeState State { get; set; }

        public event Action<SkillNodeUI> OnClick = delegate { };

        private void Awake()
        {
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
            SetNodeActive();
            Debug.Log(" Включаем состояние ноды - замакшена");
        }

        private void ActivateSelf()
        {
            gameObject.SetActive(true);
        }

        public void SetNodeActive()
        {
            Background.color = ActiveNodeColor;
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

        private void DeactivateNode()
        {
        }

        private void SetUnreachable()
        {
            Background.color = NodeUnreachableColor;
            //todo: find out
        }

        private void SetNoMoney()
        {
            Background.color = NoMoneyNodeColor;
        }

        private void SetCanBuy()
        {
            Background.color = CanBuyNodeColor;
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

        public void HidePrice()
        {
            Price.gameObject.SetActive(false);
            PriceIcon.gameObject.SetActive(false);
        }
    }
}