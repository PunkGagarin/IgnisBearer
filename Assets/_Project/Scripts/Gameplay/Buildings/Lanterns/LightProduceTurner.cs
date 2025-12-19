namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LightProduceTurner : BaseProducerSwitcher
    {
        private Lantern _lantern;


        protected override void Awake()
        {
            base.Awake();
            _lantern = GetComponent<Lantern>();
        }

        protected override bool IsReadyToProduce()
        {
            return _lantern.IsFired();
        }

    }
}