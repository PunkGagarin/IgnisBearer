namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchBuilding : Building
    {
        private IWorkersCapacity _workersCapacity;

        private IFateProducer _fateProducer;
        private IFateStorage _fateStorage;
        
        private ILightStorage _lightStorage;

        protected override void Awake()
        {
            _workersCapacity = GetComponent<IWorkersCapacity>();
            _lightStorage = GetComponent<ILightStorage>();
            _fateProducer = GetComponent<IFateProducer>();
            _fateStorage = GetComponent<IFateStorage>();
        }

        public void Init()
        {
        }

        public void PutLight(int count)
        {
            _lightStorage.IncrementAmount(count);
        }
    }
}