using System.Collections.Generic;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitStat : Stat<UnitStatType>, IUnitStat
    {
        public UnitStat(UnitStatType type, float baseValue) : base(type, baseValue)
        {
        }

        public UnitStat(UnitStatType type, float baseValue, List<StatModifier> modifiers) : base(type, baseValue,
            modifiers)
        {
        }
    }
}