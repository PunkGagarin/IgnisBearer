namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    public class FateProducerSwitcher : BaseProducerSwitcher
    {
        private Workers _workers;

        protected override void Awake()
        {
            base.Awake();
            _workers = GetComponent<Workers>();
        }

        protected override bool IsReadyToProduce()
        {
            return _workers.HasAnyWorker();
        }
    }
}