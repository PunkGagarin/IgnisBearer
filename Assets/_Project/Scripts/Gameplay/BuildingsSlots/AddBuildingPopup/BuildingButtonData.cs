using _Project.Scripts.Gameplay.Buildings;

namespace _Project.Scripts.Gameplay.BuildingsSlots
{
    public class BuildingButtonData
    {
        public BuildingType BuildingType { get; }
        public double Price { get; }

        public BuildingButtonData(BuildingType buildingType, double price)
        {
            BuildingType = buildingType;
            Price = price;
        }
    }
}