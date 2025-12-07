namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IFateGenerator
    {
        void Init();
        void StartGenerating();
        void StopGenerating();
    }
}