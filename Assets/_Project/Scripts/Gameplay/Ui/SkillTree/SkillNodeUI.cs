using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.SkillTree
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
        private List<SkillNodeUI> NextNodes { get; set; }

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


        public void Init(Sprite icon, int price, Sprite priceIcon)
        {
            Icon.sprite = icon;
            Price.text = price.ToString();
            PriceIcon.sprite = priceIcon;

            UpdateUi();
        }

        public void UpdateUi()
        {
        }

        public void BuyNode()
        {
        }

        public void SetState(NodeBoughtState boughtState)
        {
        }

        public void SetState(SkillNodeState nodeState)
        {
            State = nodeState;
            switch (nodeState)
            {
                case SkillNodeState.Unreachable:
                    SetUnreachable();
                    break;
                case SkillNodeState.NoMoney:
                    SetNoMoney();
                    break;
                case SkillNodeState.CanBuy:
                    SetCanBuy();
                    break;
                case SkillNodeState.None:
                default:
                    throw new NotImplementedException();
            }
        }

        public void SetNodeActive()
        {
            Background.color = ActiveNodeColor;
            foreach (var arrow in ArrowsFromHere)
            {
                //activate arrow
            }
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
    }

}