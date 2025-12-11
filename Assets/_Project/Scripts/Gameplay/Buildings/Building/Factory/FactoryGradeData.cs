using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    [Serializable]
    public class FactoryGradeData: IBaseGradeData
    {
        public int GradePrice;
        public int MaxUnitsCount;
        public int MaxDurability;
    }
}