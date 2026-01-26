using UnityEngine;
using Zenject;

namespace _Project.Scripts.GD
{
    public class GdService : IInitializable
    {

        [Inject] private readonly GDSettings _settings;
        
        public void Initialize()
        {
            if (_settings.ClearPrefsOnStart)
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}