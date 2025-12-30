using System.Linq;
using _Project.Scripts.Gameplay.SkillTree;
using Zenject;

namespace _Project.Scripts.Gameplay.Data
{
    public class SkillTreeDataFacade
    {
        [Inject] private PlayerDataService _dataService;
        [Inject] private SkillTreeFactory _factory;

        public SkillTreeData TreeData => _dataService.PlayerData.SkillTreeData;

        public NodeBoughtState GetBoughtStateFor(SkillNodeType nodeType)
        {
            var node = GetNode(nodeType);
            return node.BoughtState;
        }

        public SkillTreeNodeData GetNode(SkillNodeType nodeType)
        {
            if (!TreeData.Nodes.Exists(el => el.Type == nodeType))
            {
                // Debug.LogError($" у нас в сохранке нету ноды дерева с типом: {nodeType}");
                return CreateNewNode(nodeType);
            }
            return TreeData.Nodes.FirstOrDefault(el => el.Type == nodeType);
        }

        private SkillTreeNodeData CreateNewNode(SkillNodeType nodeType)
        {
            return _factory.CreateNode(nodeType);
        }

        public int GetCurrentLevel(SkillNodeType nodeType)
        {
            var node = GetNode(nodeType);
            return node.CurrentLevel;
        }

        public int GetMaxLevel(SkillNodeType nodeType)
        {
            var node = GetNode(nodeType);
            return node.MaxLevel;
        }

        public void SetBoughtState(SkillNodeType nodeType, NodeBoughtState boughtState)
        {
            var node = GetNode(nodeType);
            node.BoughtState = boughtState;
        }

        public void SetCurrentLevel(SkillNodeType nodeType, int newLevel)
        {
            var node = GetNode(nodeType);
            node.CurrentLevel = newLevel;
        }

        public void SetTreeData(SkillTreeData treeData)
        {
            _dataService.PlayerData.SkillTreeData = treeData;
        }
    }
}