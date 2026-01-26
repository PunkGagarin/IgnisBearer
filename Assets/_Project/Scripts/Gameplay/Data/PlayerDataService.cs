using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Data
{
    public class PlayerDataService : IDisposable, ISaveLoadData
    {
        private const string SaveKey = "PlayerData_one";

        public PlayerData PlayerData { get; set; } = new();

        public bool HasProgress()
        {
            return PlayerPrefs.HasKey(SaveKey);
        }

        public void Dispose()
        {
            Save();
        }

        public async UniTask Load()
        {
            string json = PlayerPrefs.GetString(SaveKey);
            PlayerData = await UniTask.RunOnThreadPool(() =>
                JsonConvert.DeserializeObject<PlayerData>(json)
            );
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(PlayerData);
            PlayerPrefs.SetString(SaveKey, json);
        }
    }
}