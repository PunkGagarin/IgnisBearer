using System;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class PlayerDataService : IInitializable, IDisposable
    {
        private const string SaveKey = "PlayerData_one";

        private PlayerData _playerData;

        public void Initialize()
        {
            if (PlayerPrefs.HasKey(SaveKey))
                Load();
            else
                _playerData = new PlayerData();
        }

        public void Dispose()
        {
            Save();
        }

        private void Load()
        {
            var json = PlayerPrefs.GetString(SaveKey);
            _playerData = JsonConvert.DeserializeObject<PlayerData>(json);
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(_playerData);
            PlayerPrefs.SetString(SaveKey, json);
        }
    }
}