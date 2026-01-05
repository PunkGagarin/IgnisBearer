namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public abstract class TreeNodeEffector : ITreeNodeEffector
    {
        public abstract SkillNodeType Type { get; protected set; }
        public abstract void ApplyEffect(int level);
        public abstract void RemoveEffect(int level);
    }
}