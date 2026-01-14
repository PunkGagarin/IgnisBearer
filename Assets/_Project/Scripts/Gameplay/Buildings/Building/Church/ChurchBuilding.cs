using System;
using _Project.Scripts.Gameplay.Ui.Buildings;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchBuilding : Building
    {
        [Inject] private ChurchSettings _churchSettings;
        

        protected override void Awake()
        {
            base.Awake();
            var openBuildingPopup = GetComponent<OpenBuildingPopup>();
            openBuildingPopup.SetButtonEnabled(false);
        }
        
       
        
        public float GetLightSendSpeed()
        {
            var curGrade = GetComponent<IGrade>().Current;
            var curGradeData = _churchSettings.GetData(curGrade); //todo get from save?
            return curGradeData.LightSendSpeed;
        }     
        
        public float GetQueueCapacity()
        {
            var curGrade = GetComponent<IGrade>().Current;
            var curGradeData = _churchSettings.GetData(curGrade); //todo get from save?
            return curGradeData.QueueCapacity;
        }    
        
        public int GetAmountPerLight()
        {
            var curGrade = GetComponent<IGrade>().Current;
            var curGradeData = _churchSettings.GetData(curGrade); //todo get from save?
            return curGradeData.AmountPerLight;
        }
    }
}