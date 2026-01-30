using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public interface ITreeNodeEffector : INodePersist
    {
        public SkillNodeType Type { get; }
        void ApplyEffect(int level);
        void RemoveEffect(int level);
    }

}