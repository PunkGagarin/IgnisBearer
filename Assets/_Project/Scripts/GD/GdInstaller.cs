using UnityEngine;
using Zenject;

namespace _Project.Scripts.GD
{
    public class GdInstaller : MonoInstaller
    {

        [field: SerializeField]
        public GDSettings GdSettings { get; private set; }

        public override void InstallBindings()
        {
            Container.Bind<GDSettings>().FromInstance(GdSettings).AsSingle();
        }
    }
}