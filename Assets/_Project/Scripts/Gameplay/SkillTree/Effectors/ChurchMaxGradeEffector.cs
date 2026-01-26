using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Data;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class ChurchMaxGradeEffector :
        TreeNodeEffector<ChurchMaxGradeNodeSettings, ChurchMaxGradeNodeEffectSettings>
    {
        [Inject] private readonly BuildingsService _buildingsService;
        [Inject] private readonly PlayerDataService _playerDataService;

        public override SkillNodeType Type { get; protected set; } = SkillNodeType.ChurchMaxGrade;

        protected override void AddEffect(ChurchMaxGradeNodeEffectSettings effectSettings)
        {
            _playerDataService.PlayerData.BuildingData.ChurchData.MaxGradeLevel = effectSettings.MaxGrade;
            //go to church and update it
            //church.Grade.SetMaxLevel ()
        }
    }
}