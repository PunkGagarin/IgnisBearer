using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Data;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class AddAutoLighterEffector :
        TreeNodeEffector<AddAutoLighterNodeSettings, AddAutoLighterNodeEffectSettings>
    {
        [Inject] private readonly PlayerDataService _playerDataService;

        public override SkillNodeType Type { get; protected set; } = SkillNodeType.AddAutoLighter;

        protected override void AddEffect(AddAutoLighterNodeEffectSettings effectSettings)
        {
            _playerDataService.PlayerData.BuildingData.AvailableBuildings[BuildingType.AutoLighter] = 1;
        }
    }
}