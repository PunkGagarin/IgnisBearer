using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui.Tooltips
{
    [CreateAssetMenu(fileName = "TooltipUiData", menuName = "Game Resources/TooltipUiData")]    
    public class TooltipUiData: ScriptableObject
    {
          [field: SerializeField] public string TitleKey { get; set; }
          [field: SerializeField] public int Level { get; set; }
          [field: SerializeField] public string LevelDescKey { get; set; }
          [field: SerializeField] public string DescriptionKey { get; set; }
          [field: SerializeField] public float Price { get; set; }
    }
}