using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Data;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class AddAutoHarvestEffector :
        TreeNodeEffector<AddAutoHarvestNodeSettings, AddAutoHarvestNodeEffectSettings>
    {
        [Inject] private readonly PlayerDataService _playerDataService;

        public override SkillNodeType Type { get; protected set; } = SkillNodeType.AddAutoHarvest;

        protected override void AddEffect(AddAutoHarvestNodeEffectSettings effectSettings)
        {
            _playerDataService.PlayerData.BuildingData.AvailableBuildings[BuildingType.AutoHarvest] = 1;
        }
    }
}