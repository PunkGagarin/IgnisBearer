namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public interface ILanternSlotCountInfluencer : INodePersist
    {
        StatModifier GetSlotsCount();
    }
}