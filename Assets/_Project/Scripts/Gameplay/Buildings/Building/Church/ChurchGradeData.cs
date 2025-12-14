using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    [Serializable]
    public class ChurchGradeData: IBaseGradeData
    {

        public int MaxUnitsCount;
        public int MaxLightStorageCapacity = 1000;
        public int GradePrice;
        public int TimeToProduceFate;
        public int AmountToProduceFateAtTime;
        public float LightSendSpeed = 3f;
    }
}