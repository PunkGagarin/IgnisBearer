namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public interface ITreeNodeEffector
    {
        public SkillNodeType Type { get; }
        void ApplyEffect(int level);
        void RemoveEffect(int level);
    }
}