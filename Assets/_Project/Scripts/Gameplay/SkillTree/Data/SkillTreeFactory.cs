using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeFactory
    {
        [Inject] private SkillTreeSettings _settings;
        [Inject] private SkillTreeUi _ui;

        public SkillTreeData Create()
        {
            var treeData = new SkillTreeData();


            if (_settings.SkillNodeSettings.Count < _ui.SkillNodeUIs.Count)
            {
                Debug.LogError($"Количество скиллов в настройках {_settings.SkillNodeSettings.Count} " +
                               $"не совпадает с количеством в UI {_ui.SkillNodeUIs.Count}");
            }

            var initNodeType = _settings.InitNode;
            
            var node = CreateNode(initNodeType);
            treeData.Nodes.Add(node);

            return treeData;
        }

        public SkillTreeNodeData CreateNode(SkillNodeType initNodeType)
        {
            SkillNodeUI nodeUi = _ui.SkillNodeUIs.FirstOrDefault(el => el.Type == initNodeType);

            if (!nodeUi)
            {
                Debug.LogError($"Начальный узел {initNodeType} не найден, проверь настройки и сами ноды!");
                throw new Exception();
            }

            SkillTreeNodeData nodeData = new SkillTreeNodeData();
            nodeData.Type = initNodeType;
            nodeData.BoughtState = NodeBoughtState.None;
            nodeData.NextNodes = nodeUi.NextNodes.Select(el => el.Type).ToList();
            nodeData.MaxLevel = _settings.GetMaxLevelFor(initNodeType);
            return nodeData;
        }
    }
}