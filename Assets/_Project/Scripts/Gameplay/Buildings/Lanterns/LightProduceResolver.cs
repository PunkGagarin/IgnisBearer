namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LightProduceResolver : BaseProducerResolver
    {
        private Lantern _lantern;

        protected void Awake()
        {
            _lantern = GetComponent<Lantern>();
        }

        public override bool CanProduce()
        {
            return _lantern.IsFired();
        }

    }
}