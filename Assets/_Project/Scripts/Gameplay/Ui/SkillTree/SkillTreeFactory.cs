using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.SkillTree
{
    public class SkillTreeFactory
    {
        [Inject] private SkillTreeSettings _settings;

        public SkillTreeData Create(SkillTreeUi ui)
        {
            var treeData = new SkillTreeData();
            var nodeSettings = _settings.SkillNodeSettings;

            if (nodeSettings.Count != ui.SkillNodeUIs.Count)
            {
                Debug.LogError(
                    $" Количество скиллов в настройках {nodeSettings.Count} не совпадает с количеством в UI {ui.SkillNodeUIs.Count}");
                throw new Exception();
            }

            foreach (var nodeUI in ui.SkillNodeUIs)
            {
                SkillTreeNodeData nodeData = new SkillTreeNodeData();
                // nodeData
                treeData.Nodes.Add(nodeData);
            }

            return new SkillTreeData();
        }
    }
}