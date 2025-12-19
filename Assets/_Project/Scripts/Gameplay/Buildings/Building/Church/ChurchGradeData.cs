using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    [Serializable]
    public class ChurchGradeData: IBaseGradeData
    {

        public int MaxLightStorageCapacity = 1000;
        public int GradePrice;
        public float LightSendSpeed = 3f;
    }
}