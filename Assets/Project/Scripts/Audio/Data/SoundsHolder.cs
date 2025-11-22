using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Audio.Data
{
    [CreateAssetMenu(fileName = "SoundsHolder", menuName = "Audio/SoundsHolder")]
    public class SoundsHolder : ScriptableObject
    {
        [field: SerializeField] public List<AudioClip> Sounds { get; private set; }
    }
}
