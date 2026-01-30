namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public interface IUnitMoveInfluencer : INodePersist
    {
        StatModifier GetSpeedModifier();
    }
}