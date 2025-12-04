using _Project.Scripts.Gameplay.Buildings;

namespace _Project.Scripts.Gameplay.BuildingsSlots
{
    public class BuildingButtonData
    {
        public BuildingType BuildingType { get; }
        public double Price { get; }
        public string Label { get; }

        public BuildingButtonData(BuildingType buildingType, double price, string label)
        {
            BuildingType = buildingType;
            Price = price;
            Label = label;
        }
    }
}