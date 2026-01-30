using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree.Effectors
{
    public class WorkerMoveSpeedEffector :
        TreeNodeEffector<WorkerMoveSpeedNodeSettings, WorkerMoveSpeedNodeEffectSettings>, IUnitMoveInfluencer
    {
        [Inject] private WorkerService _workerService;
        private string Source => GetType().ToString();
        public override SkillNodeType Type { get; protected set; } = SkillNodeType.WorkerMoveSpeed;

        protected override void AddEffect(WorkerMoveSpeedNodeEffectSettings effectSettings)
        {
            var statModifier = CreateStatFor(effectSettings);
            _workerService.AddModToAllUnits(statModifier);
        }

        private StatModifier CreateStatFor(WorkerMoveSpeedNodeEffectSettings effectSettings)
        {
            var statModifier = new StatModifier(ModifierType.Multiply, effectSettings.MoveSpeedMultiplier, Source);
            return statModifier;
        }

        protected override void RemoveEffect(WorkerMoveSpeedNodeEffectSettings effectSettings)
        {
            _workerService.RemoveModFromAllUnits(Source);
            Debug.LogError($"House capacity should be removed before applying new effect by: " +
                           $"{effectSettings?.MoveSpeedMultiplier} ");
        }

        public StatModifier GetSpeedModifier()
        {
            return CreateStatFor(EffectSettings(GetCurrentLevel()));
        }
    }
}