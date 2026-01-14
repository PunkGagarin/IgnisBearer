using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class AddAutoHarvesterBuildingEffector : 
        TreeNodeEffector<AddAutoHarvesterBuildingNodeSettings, AddAutoHarvesterBuildingEffectSettings>
    {
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.HouseCapacity;

        protected override void AddEffect(AddAutoHarvesterBuildingEffectSettings effectSettings)
        {
            Debug.LogError($"AutoHarvesterBuilding will be added after impl by: " +
                           $"");
        }

        protected override void RemoveEffect(AddAutoHarvesterBuildingEffectSettings effectSettings)
        {
        }
    }
}