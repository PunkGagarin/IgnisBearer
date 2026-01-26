using _Project.Scripts.Gameplay.Data;
using _Project.Scripts.Gameplay.SkillTree.Effectors;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SkillTree
{
    public class SkillTreeInstaller : MonoInstaller
    {
        [field: SerializeField]
        public SkillTreeUi SkillTreeUi { get; private set; }

        [field: SerializeField]
        public SkillTreeSettings Settings { get; private set; }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SkillTreeUi>().FromInstance(SkillTreeUi).AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeSettings>().FromInstance(Settings).AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeNodeEffectorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeDataFacade>().AsSingle();

            BindEffectors();
        }

        private void BindEffectors()
        {
            Container.BindInterfacesAndSelfTo<HouseCapacityEffector>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChurchMaxGradeEffector>().AsSingle();
            Container.BindInterfacesAndSelfTo<LanternGenerationSpeedEffector>().AsSingle();
            Container.BindInterfacesAndSelfTo<WorkerMoveSpeedEffector>().AsSingle();
        }
    }
}