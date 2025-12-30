using _Project.Scripts.Gameplay.Data;
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
            Container.Bind<SkillTreeUi>().FromInstance(SkillTreeUi).AsSingle();
            Container.Bind<SkillTreeSettings>().FromInstance(Settings).AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillTreeDataFacade>().AsSingle();
        }
    }
}