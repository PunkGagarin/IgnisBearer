using System.Collections.Generic;

namespace _Project.Scripts.Gameplay.Buildings.BuildingSlots
{
    public class BuildingSlotStat : Stat<BuildingSlotStatType>, IBuildingSlotStat
    {
        public BuildingSlotStat(BuildingSlotStatType type, float baseValue) : base(type, baseValue)
        {
        }

        public BuildingSlotStat(BuildingSlotStatType type, float baseValue, List<StatModifier> modifiers) : base(type,
            baseValue,
            modifiers)
        {
        }
    }
}