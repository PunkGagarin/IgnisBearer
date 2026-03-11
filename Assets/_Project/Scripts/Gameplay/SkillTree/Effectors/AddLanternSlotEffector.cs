namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class AddLanternSlotEffector :
        TreeNodeEffector<AddLanternSlotNodeSettings, AddLanternSlotNodeEffectSettings>,
        ILanternSlotCountInfluencer
    {
        private string Source => GetType().ToString();
        
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.AddLanternSlot;

        protected override void AddEffect(AddLanternSlotNodeEffectSettings effectSettings)
        {
        }

        protected override void RemoveEffect(AddLanternSlotNodeEffectSettings effectSettings)
        {
        }

        private StatModifier CreateStatFor(AddLanternSlotNodeEffectSettings effectSettings)
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