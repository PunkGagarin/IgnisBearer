using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public abstract class Stat<ST> : IStat<ST>
    {
        public ST Type { get; private set; }
        public float BaseValue { get; private set; }
        private List<StatModifier> _modifiers = new();

        protected Stat(ST type, float baseValue)
        {
            Type = type;
            BaseValue = baseValue;
        }

        protected Stat(ST type, float baseValue, List<StatModifier> modifiers)
        {
            Type = type;
            BaseValue = baseValue;
            _modifiers.AddRange(modifiers);
        }

        public float GetValue()
        {
            float finalSum = 0f;
            float finalMultipy = 1f;

            foreach (var mod in _modifiers)
            {
                if (mod.Type == ModifierType.Sum)
                {
                    finalSum += mod.Value;
                }
                else if (mod.Type == ModifierType.Multiply)
                {
                    finalMultipy *= mod.Value;
                }
            }

            return (BaseValue + finalSum) * finalMultipy;
        }

        public int GetRoundedValue()
        {
            return Mathf.CeilToInt(GetValue());
        }

        public void AddModifier(StatModifier mod)
        {
            _modifiers.Add(mod);
        }

        public void RemoveModifier(string source)
        {
            StatModifier mod = _modifiers.Find(mod => mod.Source == source);

            if (mod != null)
                _modifiers.Remove(mod);
            else
            {
                var strList =
                    _modifiers.Select(el => new CustomKeyValue<string, string>(el.Source, el.Value.ToString()));
                string modsInString = string.Join(", ", strList);
                Debug.LogError($"Trying to remove mod which is not exists! " +
                               $"type: {Type}, trying to remove: {source}, real mods: {modsInString}");
            }
        }
    }
}