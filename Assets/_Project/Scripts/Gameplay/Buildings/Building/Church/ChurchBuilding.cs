using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchBuilding : Building
    {
        [Inject] private ChurchSettings _churchSettings;

        public float GetLightSendSpeed()
        {
            var curGrade = GetComponent<IGrade>().Current;
            var curGradeData = _churchSettings.GetData(curGrade); //todo get from save?
            return curGradeData.LightSendSpeed;
        }
    }
}