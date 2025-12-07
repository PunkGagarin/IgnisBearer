using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Infrastructure.GameStates.States;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class LevelService
    {
        
        [Inject]
        private LevelFactory _levelFactory;

        private LevelInfo _level;

        public void CreateLevel()
        {
            _level = _levelFactory.CreateLevel();
        }

        public List<LanternSpawnPoint> GetInitialLanternPositions()
        {
            return _level.InitalLanternPositions;
        }

        public UnitSpawnPoint GetInitalUnitPosition()
        {
            return _level.InitalUnitPosition;
        }

        public List<BuildingSlotsSpawnPoint> GetInitialBuildingsSpawnPoints()
        {
            return _level.InitalBuildingSlotsPositions;
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
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                0);
        }

        public void GetMapBounds()
        {
            var bg = _level.Background;
        }
    }
}