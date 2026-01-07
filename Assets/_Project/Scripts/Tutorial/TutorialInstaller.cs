using UnityEngine;
using Zenject;

namespace _Project.Scripts.Tutorial
{
    public class TutorialInstaller : MonoInstaller
    {
        [field: SerializeField]
        public TutorialUi Ui { get; private set; }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TutorialUi>().FromInstance(Ui).AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialStepFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialService>().AsSingle();
        }
    }
}