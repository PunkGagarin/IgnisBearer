using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.SkillTree
{
    public partial class SkillTreeService : IInitializable, IDisposable, ISaveLoadData
    {
        [Inject] private SkillTreeUi _ui;
        [Inject] private SkillTreeFactory _factory;
        [Inject] private PlayerDataService _playerDataService;

        public void Initialize()
        {
        }

        public void Dispose()
        {
            
        }

        public void Load()
        {
            var context = _playerDataService.PlayerData;
        }

        public void Save()
        {
            
        }

        public void Init(SkillTreeData data)
        {
            
        }

        public void Create()
        {
            var treeData = _factory.Create(_ui);
            Init(treeData);
        }

    }

    public class SkillTreeFactory
    {
        [Inject] private SkillTreeSettings _settings;
        
        public SkillTreeData Create(SkillTreeUi ui)
        {
            var treeData = new SkillTreeData();
            var nodeSettings = _settings.SkillNodeSettings;

            if (nodeSettings.Count != ui.SkillNodeUIs.Count)
            {
                Debug.LogError($" Количество скиллов в настройках {nodeSettings.Count} не совпадает с количеством в UI {ui.SkillNodeUIs.Count}");
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

    [CreateAssetMenu(fileName = "SkillTree", menuName = "Gameplay/SkillTree", order = 1)]
    public class SkillTreeSettings : ScriptableObject
    {
        [field: SerializeField]
        public List<SkillNodeSettings> SkillNodeSettings { get; set; }
    }

    [Serializable]
    public class SkillNodeSettings
    {
        [field: SerializeField]
        public SkillNodeType NodeType { get; private set; }

        [field: SerializeField]
        public int MaxLevel { get; private set; }

    }

    public class NodeInfo
    {
        //соседи
        //парент
        //effect
    }
}