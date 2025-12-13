using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Level;
using Zenject;

namespace _Project.Scripts.Infrastructure.GameStates.States
{
    public class LevelFactory
    {
        [Inject] private DiContainer _container;
        [Inject] private LevelData _levelData;

        public LevelInfo CreateLevel()
        {
            var level = _container.InstantiatePrefabForComponent<LevelInfo>(_levelData.LevelInfo);
            return level;
        }
    }
}