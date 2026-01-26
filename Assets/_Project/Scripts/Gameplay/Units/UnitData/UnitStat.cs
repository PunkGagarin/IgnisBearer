namespace _Project.Scripts.Gameplay.Units
{
    public class UnitStat : Stat<UnitStatType>, IUnitStat
    {
        public UnitStat(UnitStatType type, float baseValue) : base(type, baseValue)
        {
        }
    }
}