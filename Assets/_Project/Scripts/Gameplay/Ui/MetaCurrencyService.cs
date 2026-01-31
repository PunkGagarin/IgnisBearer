using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui
{
    public class MetaCurrencyService
    {
        private Dictionary<MetaCurrencyType, int> _bank = new();

        public void Init(Dictionary<MetaCurrencyType, int> currencyDataCurrencies)
        {
            MetaCurrencyType[] allCurrencyTypes = (MetaCurrencyType[])Enum.GetValues(typeof(MetaCurrencyType));

            foreach (var currencyType in allCurrencyTypes.Where(currencyType => currencyType != MetaCurrencyType.None))
                _bank.Add(currencyType, 0);
        }

        public bool HasEnough(MetaCurrencyType type, int amount)
        {
            return _bank[type] >= amount;
        }


        public void Add(MetaCurrencyType type, int amount)
        {
            _bank[type] += amount;
        }

        public bool TrySpend(MetaCurrencyType type, int amount)
        {
            if (!HasEnough(type, amount))
            {
                // Debug.LogError();
                return false;
            }

            _bank[type] -= amount;
            return true;
        }

        public void Spend(MetaCurrencyType type, int amount)
        {
            if (!HasEnough(type, amount))
            {
                Debug.LogError(" Не должны были вызывать этот метод без наличия денег!!");
                return;
            }
            _bank[type] -= amount;
        }
    }

}