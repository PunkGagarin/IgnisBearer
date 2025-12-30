using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeUi : ContentUi
    {
        
        //todo: remove from here
        [field: SerializeField]
        public Button OpenButton { get; private set; }

        [field: SerializeField]
        public List<Button> CloseButtons { get; private set; }

        [field: SerializeField]
        public List<SkillNodeUI> SkillNodeUIs { get; private set; }

        public event Action<SkillNodeUI> OnNodeClicked = delegate { };

        private void Awake()
        {
            OpenButton.onClick.AddListener(Show);
            foreach (var closeButton in CloseButtons)
                closeButton.onClick.AddListener(Hide);

            foreach (var node in SkillNodeUIs)
                node.OnClick += OnNodeClickedHandle;
        }

        private void OnDestroy()
        {
            OpenButton.onClick.RemoveListener(Show);
            foreach (var closeButton in CloseButtons)
                closeButton.onClick.RemoveListener(Hide);

            foreach (var node in SkillNodeUIs)
                node.OnClick -= OnNodeClickedHandle;
        }

        private void OnNodeClickedHandle(SkillNodeUI node)
        {
            OnNodeClicked.Invoke(node);
        }

        public void SetNodeState1(SkillNodeType nodeDataType, NodeBoughtState nodeDataBoughtState,
            int nodeDataCurrentLevel, int nodeDataMaxLevel)
        {
            var nodeUi = SkillNodeUIs.FirstOrDefault(el => el.Type == nodeDataType);
            if (nodeUi == null)
            {
                Debug.LogError($"Не могу найти ноду {nodeDataType} в UI!");
                return;
            }
            nodeUi.SetState(nodeDataBoughtState);
            nodeUi.SetNewLevel(nodeDataCurrentLevel);
            nodeUi.SetMaxLevel(nodeDataMaxLevel);
        }

        public void InitNode(SkillNodeType nodeDataType, NodeBoughtState nodeDataBoughtState, int nodeDataCurrentLevel, int nodeDataMaxLevel, int price, MetaCurrencyType currencyType, Sprite Icon)
        {
            var nodeUi = SkillNodeUIs.FirstOrDefault(el => el.Type == nodeDataType);
            if (nodeUi == null)
            {
                Debug.LogError($"Не могу найти ноду {nodeDataType} в UI!");
                return;
            }
            nodeUi.SetState(nodeDataBoughtState);
            nodeUi.SetNewLevel(nodeDataCurrentLevel);
            nodeUi.SetMaxLevel(nodeDataMaxLevel);
            nodeUi.SetPrice(price);
            nodeUi.SetCurrencyType(currencyType);
            nodeUi.SetIcon(Icon);
        }
    }
}