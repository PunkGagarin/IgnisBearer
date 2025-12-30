using System;
using _Project.Scripts.Gameplay.Data;
using _Project.Scripts.Gameplay.Ui;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeService : IInitializable, IDisposable
    {
        [Inject] private SkillTreeUi _ui;
        [Inject] private SkillTreeFactory _factory;
        [Inject] private SkillTreeDataFacade _skillTreeData;
        [Inject] private SkillTreeSettings _settings;
        [Inject] private MetaCurrencyService _metaCurrencyService;

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
            if (CanBuyNode(node, out var currency, out var price))
            {
                _metaCurrencyService.Spend(currency, price);

                int newLevel = GetCurrentNodeLevel(node.Type) + 1;
                _skillTreeData.SetCurrentLevel(node.Type, newLevel);

                if (ActivatingFirstTime(node.Type))
                    node.SetNodeActive();

                if (newLevel == GetMaxNodeLevel(node.Type))
                    SetMaxLevel(node);

                node.SetNewLevel(newLevel);
            }
            else
            {
                //todo: cant buy sound
                node.StartCantBuyAnimation();
            }
        }

        private void SetMaxLevel(SkillNodeUI node)
        {
            NodeBoughtState boughtState = NodeBoughtState.Maxed;
            _skillTreeData.SetBoughtState(node.Type, boughtState);
            node.SetState(boughtState);
        }

        private bool ActivatingFirstTime(SkillNodeType type)
        {
            return GetBoughtState(type) == NodeBoughtState.NotBought;
        }

        private int GetMaxNodeLevel(SkillNodeType nodeType)
        {
            return _skillTreeData.GetMaxLevel(nodeType);
        }

        private bool CanBuyNode(SkillNodeUI node, out MetaCurrencyType currencyType, out int price)
        {
            currencyType = _settings.GetCurrencyTypeFor(node.Type);
            int currentLevel = GetCurrentNodeLevel(node.Type);
            price = _settings.GetPriceFor(node.Type, currentLevel);
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


        public void Create()
        {
            var treeData = _factory.Create();
            _skillTreeData.SetTreeData(treeData);
            Init(treeData);
        }

        public void Init(SkillTreeData data)
        {
            foreach (var nodeData in data.Nodes)
            {
                _ui.InitNode(nodeData.Type, 
                    nodeData.BoughtState, 
                    nodeData.CurrentLevel, 
                    nodeData.MaxLevel,
                    _settings.GetPriceFor(nodeData.Type, nodeData.CurrentLevel),
                    _settings.GetCurrencyTypeFor(nodeData.Type), 
                    _settings.GetIconFor(nodeData.Type));
            }
        }
    }
}