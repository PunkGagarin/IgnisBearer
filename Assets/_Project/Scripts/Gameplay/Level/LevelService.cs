using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Infrastructure.GameStates.States;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Level
{
    public class LevelService : IBuildContainer
    {
        private const float EdgeOffset = 5;
        private const float TopEdgeOffset = 10;

        [Inject]
        private LevelFactory _levelFactory;

        private LevelInfo _level;

        public void CreateLevel()
        {
            _level = _levelFactory.CreateLevel();
        }

        public List<LanternSlotSpawnPoint> GetInitialLanternSlotsPositions()
        {
            return _level.InitalLanternSlotPositions;
        }

        public List<LanternSlotSpawnPoint> GetAdditionalLanternSlotsPositions()
        {
            return _level.LanternSlotsPositions;
        }

        public UnitSpawnPoint GetInitalUnitPosition()
        {
            return _level.InitalUnitPosition;
        }

        public List<BuildingSlotsSpawnPoint> GetInitialBuildingsSpawnPoints()
        {
            return _level.BuildingSlotsPositions;
        }

        public BuildingSlotsSpawnPoint GetChurchBuildingSpawnPoint()
        {
            return _level.ChurchBuildingSlotPosition;
        }

        public Vector3 GetRandomMapPosition()
        {
            var bg = _level.Background;
            Bounds bounds = bg.bounds;

            //check if inside of building bounds
            return new Vector3(
                Random.Range(bounds.min.x + EdgeOffset, bounds.max.x - EdgeOffset),
                Random.Range(bounds.min.y + EdgeOffset, bounds.max.y - TopEdgeOffset),
                0);
        }

        public void GetMapBounds()
        {
            var bg = _level.Background;
        }

        public Transform GetSlotsContainer()
        {
            return _level.SlotsContainer;
        }

        public Transform GetBuildingContainer()
        {
            return _level.BuildingsContainer;
        }
    }
}