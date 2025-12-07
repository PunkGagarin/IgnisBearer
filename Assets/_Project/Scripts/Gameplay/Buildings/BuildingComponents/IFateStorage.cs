namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IFateStorage
    {
        int Count { get; }
        int MaxCount { get; }
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
    }
}