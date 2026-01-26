using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree
{
    // [CreateAssetMenu(fileName = "WorkerMoveSpeedNodeSettings", menuName = "Gameplay/WorkerMoveSpeedNodeSettings", order = 1)]
    public class WorkerMoveSpeedNodeSettings : SkillTreeNodeWithEffectSettings<WorkerMoveSpeedNodeEffectSettings>
    {
        
    }

    [Serializable]
    public class WorkerMoveSpeedNodeEffectSettings
    {
        [field: SerializeField]
        public float MoveSpeedMultiplier { get; private set; }
    }

}