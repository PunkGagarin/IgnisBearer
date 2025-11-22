using System.Collections.Generic;
using Project.Scripts.GameplayData.Definitions;
using UnityEngine;

namespace Project.Scripts.GameplayData.Repositories
{
    public abstract class Repository<T> : ScriptableObject where T : Definition
    {
        [SerializeField] private List<T> _definitions;
        
        public List<T> Definitions => _definitions;
    }
}
