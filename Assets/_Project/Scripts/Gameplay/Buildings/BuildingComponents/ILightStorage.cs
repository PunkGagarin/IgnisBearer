namespace _Project.Scripts.Gameplay.Buildings
{
    public interface ILightStorage
    {
        int Amount { get; }
        void Init(int maxStorageCapacity);
        void IncrementAmount(int amount);
        void IncrementAmount();
        bool NotFull();
    }
}