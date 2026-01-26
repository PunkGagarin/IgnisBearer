using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.SkillTree.Effectors;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeNodeEffectorService : IInitializable
    {
        private Dictionary<SkillNodeType, ITreeNodeEffector> _effectors = new();

        [Inject] private List<ITreeNodeEffector> _nodeEffectors = new();


        public void Initialize()
        {
            _effectors = _nodeEffectors.ToDictionary(el => el.Type);
        }

        public void ActivateEffectFor(SkillNodeType nodeType, int newLevel)
        {
            if (newLevel > 1)
                RemoveEffectFor(nodeType, newLevel - 1);
            else
                AddEffectFor(nodeType, newLevel);
        }

        private void RemoveEffectFor(SkillNodeType nodeType, int level)
        {
            _effectors[nodeType].RemoveEffect(level);
        }

        public void AddEffectFor(SkillNodeType nodeType, int newLevel)
        {
            _effectors[nodeType].ApplyEffect(newLevel);
        }
    }
}