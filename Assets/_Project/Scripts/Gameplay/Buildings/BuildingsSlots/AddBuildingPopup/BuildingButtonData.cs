namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class BuildingButtonData
    {
        public BuildingType BuildingType { get; }
        public double Price { get; }
        public string LabelKey { get; }

        public BuildingButtonData(BuildingType buildingType, double price, string labelKey)
        {
            BuildingType = buildingType;
            Price = price;
            LabelKey = labelKey;
        }
    }
}