using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{

    [RequireComponent(typeof(Lantern))]
    [RequireComponent(typeof(LanternClickDetector))]
    public class LanternLightStorage : MonoBehaviour
    {

        private LanternClickDetector _clickDetector;

        private int _currentAmount;
        private int _maxAmount = 1;

        private void Awake()
        {
            _clickDetector = GetComponent<LanternClickDetector>();
        }

        public void IncrementCurrentAmount()
        {
            _currentAmount++;
            _clickDetector.TurnOnClick();
        }

        public bool NotFull()
        {
            return _currentAmount < _maxAmount;
        }

        public bool IsReadyToHarvest()
        {
            return IsFull();
        }

        private bool IsFull()
        {
            return !NotFull();
        }

        public int Harvest()
        {
            int amount = _currentAmount;
            Debug.Log("Lantern harvested " + _currentAmount);

            _currentAmount = 0;
            _clickDetector.TurnOffClick();
            return amount;
        }
    }
}