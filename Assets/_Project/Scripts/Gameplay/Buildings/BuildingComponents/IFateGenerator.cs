namespace _Project.Scripts.Gameplay.Buildings.BuildingComponents
{
    public interface IFateGenerator
    {
        void Init();
        void StartGenerating();
        void StopGenerating();
    }
}