namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IBuildInfo
    {
        BuildingType Type { get; }
        int MaxCountToBuild { get; }
        string BuildingNameKey { get; }
        float BuildPrice { get; }
    }
}