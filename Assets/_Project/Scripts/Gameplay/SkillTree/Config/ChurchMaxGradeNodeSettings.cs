using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "ChurchMaxGradeNodeSettings", menuName = "Gameplay/ChurchMaxGradeNodeSettings",
        // order = 1)]
    public class ChurchMaxGradeNodeSettings : SkillTreeNodeWithEffectSettings<ChurchMaxGradeNodeEffectSettings>
    {
    }


    [Serializable]
    public class ChurchMaxGradeNodeEffectSettings
    {
        [field: SerializeField]
        public int MaxGrade { get; private set; }
    }
}