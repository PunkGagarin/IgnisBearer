using System;
using _Project.Scripts.Gameplay.Ui;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Data
{

    public class PlayerDataService : IInitializable, IDisposable, ISaveLoadData
    {
        private const string SaveKey = "PlayerData_one";

        public PlayerData PlayerData { get; set; }

        public void Initialize()
        {
            if (PlayerPrefs.HasKey(SaveKey))
                Load();
            else
                PlayerData = new PlayerData();
        }

        public void Dispose()
        {
            Save();
        }

        public void Load()
        {
            string json = PlayerPrefs.GetString(SaveKey);
            PlayerData = JsonConvert.DeserializeObject<PlayerData>(json);
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(PlayerData);
            PlayerPrefs.SetString(SaveKey, json);
        }
    }
}