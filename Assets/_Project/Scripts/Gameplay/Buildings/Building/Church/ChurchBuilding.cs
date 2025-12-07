using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.WorkersCapacity;

namespace _Project.Scripts.Gameplay.Buildings.Church
{
    public class ChurchBuilding : Building
    {
        private IWorkersCapacity _workersCapacity;
        /*private IFateGenerator _fateGenerator;
        private IFateStorage _fateStorage;
        private IChurchLightStorage _lightStorage;*/

        private void Awake()
        {
            _workersCapacity = GetComponent<IWorkersCapacity>();
            /*_fateGenerator = GetComponent<IFateGenerator>();
            _fateStorage = GetComponent<IFateStorage>();
            _lightStorage = GetComponent<IChurchLightStorage>();*/
        }

        public void Init()
        {
        }

        public void PutLight(int count)
        {
            // _lightStorage.IncrementAmount(count);
        }
    }
}