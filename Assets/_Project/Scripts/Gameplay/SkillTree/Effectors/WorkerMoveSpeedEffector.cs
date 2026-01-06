using UnityEngine;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class WorkerMoveSpeedEffector : TreeNodeEffector<WorkerMoveSpeedNodeSettings, WorkerMoveSpeedNodeEffectSettings>
    {
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.WorkerMoveSpeed;

        protected override void AddEffect(WorkerMoveSpeedNodeEffectSettings effectSettings)
        {
            Debug.LogError($"House capacity will be increased after impl by: " +
                           $"{effectSettings?.MoveSpeedMultiplier}");
        }

        protected override void RemoveEffect(WorkerMoveSpeedNodeEffectSettings effectSettings)
        {
            Debug.LogError($"House capacity should be removed before applying new effect by: " +
                           $"{effectSettings?.MoveSpeedMultiplier} ");
        }
    }
}