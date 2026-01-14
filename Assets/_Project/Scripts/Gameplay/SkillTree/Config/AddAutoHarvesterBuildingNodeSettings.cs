using _Project.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class AddAutoHarvesterBuildingNodeSettings : 
        SkillTreeNodeWithEffectSettings<AddAutoHarvesterBuildingEffectSettings>
    {
        
    }
    
    public class AddAutoHarvesterBuildingEffectSettings
    {
        [field: SerializeField]
        public BuildingType Type { get; private set; } = BuildingType.AutoHarvest;
    }
}