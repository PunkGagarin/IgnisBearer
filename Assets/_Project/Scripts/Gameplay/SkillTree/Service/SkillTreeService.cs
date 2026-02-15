using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Data;
using _Project.Scripts.Gameplay.Ui;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeService : IInitializable, IDisposable
    {
        [Inject] private SkillTreeUi _ui;
        [Inject] private SkillTreeDataFacade _skillTreeData;
        [Inject] private SkillTreeSettings _settings;
        [Inject] private MetaCurrencyService _metaCurrencyService;
        [Inject] private SkillTreeNodeEffectorService _nodeEffectorService;

        public void Initialize()
        {
            _ui.OnNodeClicked += HandleNodeClicked;
        }

        public void Dispose()
        {
            _ui.OnNodeClicked -= HandleNodeClicked;
        }

        private void HandleNodeClicked(SkillNodeUI node)
        {
            var boughtState = GetBoughtState(node.Type);
            if (boughtState == NodeBoughtState.Maxed)
                Debug.LogError(
                    "Купленная на максимум кнпока не должна быть доступна к покупке! (не должна быть кликабельна)");
            else
                TryBuyNode(node);
        }

        private void TryBuyNode(SkillNodeUI node)
        {
            if (CanBuyNode(node.Type, out var currency, out var price))
            {
                _metaCurrencyService.Spend(currency, price);

                int newLevel = GetCurrentNodeLevel(node.Type) + 1;
                _skillTreeData.SetCurrentLevel(node.Type, newLevel);

                if (ActivatingFirstTime(node.Type))
                    ActivateNode(node);
                
                if (newLevel == GetMaxNodeLevel(node.Type))
                {
                    SetBoughtState(node, NodeBoughtState.Maxed);
                    node.SetIcon(_settings.GetMaxedIconFor(node.Type));
                }
                else
                {
                    SetBoughtState(node, NodeBoughtState.Bought);
                    node.SetPrice(_settings.GetPriceFor(node.Type, newLevel));
                }

                node.SetNewLevel(newLevel);
                ActivateNodeEffect(node.Type, newLevel);

                UpdateAllActiveNodePrices();
            }
            else
            {
                //todo: cant buy sound
                node.StartCantBuyAnimation();
            }
        }

        private void ActivateNodeEffect(SkillNodeType nodeType, int newLevel)
        {
            _nodeEffectorService.ActivateEffectFor(nodeType, newLevel);
        }

        private void ActivateNode(SkillNodeUI node)
        {
            node.SetNodeActive();

            foreach (var nextNode in node.NextNodes)
            {
                var data = _skillTreeData.CreateNewNode(nextNode.Type);
                InitNodeUi(data);
            }
        }

        private void UpdateAllActiveNodePrices()
        {
            List<SkillTreeNodeData> nonMaxedNodes = _skillTreeData.GetNonMaxedNodes();
            foreach (var node in nonMaxedNodes)
            {
                bool canBuyNode = CanBuyNode(node.Type, out var currency, out var price);
                if (canBuyNode)
                    _ui.SetCanBuyFor(node.Type);
                else
                    _ui.SetCanNotBuyFor(node.Type);
            }
        }

        private void SetBoughtState(SkillNodeUI node, NodeBoughtState nodeBoughtState)
        {
            _skillTreeData.SetBoughtState(node.Type, nodeBoughtState);
            node.SetState(nodeBoughtState);
        }

        private bool ActivatingFirstTime(SkillNodeType type)
        {
            return GetBoughtState(type) == NodeBoughtState.None;
        }

        private int GetMaxNodeLevel(SkillNodeType nodeType)
        {
            return _skillTreeData.GetMaxLevel(nodeType);
        }

        private bool CanBuyNode(SkillNodeType type, out MetaCurrencyType currencyType, out int price)
        {
            currencyType = _settings.GetCurrencyTypeFor(type);
            int currentLevel = GetCurrentNodeLevel(type);
            price = _settings.GetPriceFor(type, currentLevel);
            return _metaCurrencyService.HasEnough(currencyType, price);
        }

        private int GetCurrentNodeLevel(SkillNodeType nodeType)
        {
            return _skillTreeData.GetCurrentLevel(nodeType);
        }

        private NodeBoughtState GetBoughtState(SkillNodeType nodeType)
        {
            return _skillTreeData.GetBoughtStateFor(nodeType);
        }

        public void Init(SkillTreeData data)
        {
            foreach (var nodeData in data.Nodes)
                InitNodeUi(nodeData);
        }

        private void InitNodeUi(SkillTreeNodeData nodeData)
        {
            var nodeUi = _ui.FindNodeUi(nodeData.Type);
            nodeUi.SetState(nodeData.BoughtState);
            nodeUi.SetNewLevel(nodeData.CurrentLevel);
            nodeUi.SetMaxLevel(nodeData.MaxLevel);
            nodeUi.SetIcon(_settings.GetDefaultIconFor(nodeData.Type));
            nodeUi.SetTooltipUiData(_settings.GetTooltipUiDataFor(nodeData.Type));

            if (nodeData.BoughtState != NodeBoughtState.Maxed)
            {
                nodeUi.SetPrice(_settings.GetPriceFor(nodeData.Type, nodeData.CurrentLevel));
                nodeUi.SetCurrencyType(_settings.GetCurrencyTypeFor(nodeData.Type));
            }
        }
    }
}