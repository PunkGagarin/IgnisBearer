namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IChurchLightStorage
    {
        int Count { get; }
        int MaxCount { get; }
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
    }
}