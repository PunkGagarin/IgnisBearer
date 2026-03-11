using System.Collections.Generic;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternSlotStat : Stat<LanternSlotStatType>, ILanternSlotStat
    {
        public LanternSlotStat(LanternSlotStatType type, float baseValue) : base(type, baseValue)
        {
        }

        public LanternSlotStat(LanternSlotStatType type, float baseValue, List<StatModifier> modifiers) : base(type,
            baseValue,
            modifiers)
        {
        }
    }
}