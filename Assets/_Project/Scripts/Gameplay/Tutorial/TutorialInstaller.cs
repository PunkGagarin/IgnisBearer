using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Tutorial
{
    public class TutorialInstaller : MonoInstaller
    {
        [field: SerializeField]
        public TutorialUi Ui { get; private set; }
        
        [field: SerializeField]
        public TutorialSettings Settings { get; private set; }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TutorialUi>().FromInstance(Ui).AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialSettings>().FromInstance(Settings).AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialStepFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialService>().AsSingle();
        }
    }
}