using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeFactory
    {
        [Inject] private SkillTreeSettings _settings;

        public SkillTreeData Create(SkillTreeUi ui)
        {
            var treeData = new SkillTreeData();


            if (_settings.SkillNodeSettings.Count < ui.SkillNodeUIs.Count)
            {
                Debug.LogError(
                    $"Количество скиллов в настройках {_settings.SkillNodeSettings.Count} не совпадает с количеством в UI {ui.SkillNodeUIs.Count}");
            }

            var initNodeType = _settings.InitNode;
            SkillNodeUI firstNode = ui.SkillNodeUIs.FirstOrDefault(el => el.Type == initNodeType);

            if (!firstNode)
            {
                Debug.LogError($"Начальный узел {initNodeType} не найден, проверь настройки и сами ноды!");
                throw new Exception();
            }

            SkillTreeNodeData initNodeData = new SkillTreeNodeData();
            initNodeData.Type = initNodeType;
            initNodeData.BoughtState = NodeBoughtState.NotBought;
            initNodeData.NextNodes = firstNode.NextNodes.Select(el => el.Type).ToList();
            initNodeData.MaxLevel = _settings.GetMaxLevelFor(initNodeType);
            treeData.Nodes.Add(initNodeData);

            return treeData;
        }
    }
}