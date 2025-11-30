using _Project.Scripts.Gameplay.Church;
using _Project.Scripts.Gameplay.House;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Installer
{
    public class BuildingsInstaller : MonoInstaller
    {
        [field: SerializeField] private ChurchSettings _churchSettings;
        [field: SerializeField] private HouseSettings _houseSettings;

        public override void InstallBindings()
        {
            Container.Bind<BuildingFactory>().AsSingle();
            Container.Bind<ChurchSettings>().FromInstance(_churchSettings).AsSingle();
            Container.Bind<HouseSettings>().FromInstance(_houseSettings).AsSingle();
        }
    }
}