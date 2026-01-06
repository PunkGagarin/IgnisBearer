namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class BuildingButtonData
    {
        public BuildingType BuildingType { get; }
        public double Price { get; }
        public string LabelKey { get; }
        public bool IsEnabled { get; }

        public BuildingButtonData(BuildingType buildingType, double price, string labelKey, bool isEnabled)
        {
            BuildingType = buildingType;
            Price = price;
            LabelKey = labelKey;
            IsEnabled = isEnabled;
        }
    }
}