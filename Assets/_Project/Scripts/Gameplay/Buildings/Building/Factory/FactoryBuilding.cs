namespace _Project.Scripts.Gameplay.Buildings
{
    public class FactoryBuilding : Building
    {
        private IDurability _durability;

        protected override void Awake()
        {
            base.Awake();
            _durability = GetComponent<IDurability>();
            _durability.OnDestroyed += OnBuildingBroke;
        }
        private void OnBuildingBroke()
        {
            _durability.OnDestroyed -= OnBuildingBroke;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_durability != null)
                _durability.OnDestroyed -= OnBuildingBroke;
        }
        
    }
}