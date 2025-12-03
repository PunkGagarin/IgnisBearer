using _Project.Scripts.Gameplay.Units.Manager;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitsInstaller : MonoInstaller
    {

        [field: SerializeField]
        public UnitSpawnPoint UnitSpawnPoint { get; private set; }

        [field: SerializeField]
        public UnitSettings UnitSettings { get; private set; }

        public override void InstallBindings()
        {
            Container.Bind<UnitFactory>().AsSingle();
            Container.Bind<UnitSpawnPoint>().FromInstance(UnitSpawnPoint).AsSingle();
            Container.Bind<UnitSettings>().FromInstance(UnitSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<WorkerService>().AsSingle();
        }
    }

}