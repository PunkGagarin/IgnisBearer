namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class AddBuildingSlotEffector :
        TreeNodeEffector<AddBuildingSlotNodeSettings, AddBuildingSlotNodeEffectSettings>,
        IBuildingSlotCountInfluencer
    {
        private string Source => GetType().ToString();
        
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.AddBuildingSlot;

        protected override void AddEffect(AddBuildingSlotNodeEffectSettings effectSettings)
        {
        }

        protected override void RemoveEffect(AddBuildingSlotNodeEffectSettings effectSettings)
        {
        }

        private StatModifier CreateStatFor(AddBuildingSlotNodeEffectSettings effectSettings)
        {
            var statModifier = new StatModifier(ModifierType.Sum, effectSettings.SlotCount, Source);
            return statModifier;
        }

        public StatModifier GetSlotsCount()
        {
            return CreateStatFor(EffectSettings(GetCurrentLevel()));
        }
    }
}