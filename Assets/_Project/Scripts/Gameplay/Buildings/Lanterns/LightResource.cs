using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LightResource : MonoBehaviour
    {
        public Vector3 FinalPosition { get; private set; }


        public void SetFinalPosition(Vector3 pos)
        {
            FinalPosition = pos;
        }

    }
}