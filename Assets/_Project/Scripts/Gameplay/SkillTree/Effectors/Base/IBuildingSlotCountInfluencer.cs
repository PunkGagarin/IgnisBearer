namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public interface IBuildingSlotCountInfluencer : INodePersist
    {
        StatModifier GetSlotsCount();
    }
}