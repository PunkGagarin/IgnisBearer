using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LightResource : ClickableView<LightResource>
    {
        public Vector3 FinalPosition { get; private set; }


        public void SetFinalPosition(Vector3 pos)
        {
            FinalPosition = pos;
        }

        public void Harvest()
        {
            Debug.LogError("We are harvesting");
        }
    }
}