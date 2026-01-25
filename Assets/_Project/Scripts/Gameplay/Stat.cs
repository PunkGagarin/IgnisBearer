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


        public float GetValue()
        {
            float finalSum = 0f;
            float finalMultiply = 1f;
            foreach (var mod in _modifiers)
            {
                if (mod.Type == ModifierType.Sum)
                {
                    finalSum += mod.Value;
                }
                else if (mod.Type == ModifierType.Multiply)
                {
                    finalMultiply += mod.Value;
                }
            }

            return (BaseValue + finalSum) * finalMultiply;
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

    public interface IUnitStat : IStat<UnitStatType>
    {
    }

    public enum UnitStatType
    {
        None = 0,
        MoveSpeed = 1,
    }

    public interface IStat<ST>
    {
        public ST Type { get; }
        public float GetValue();
        public int GetRoundedValue();
        public void AddModifier(StatModifier mod);
        public void RemoveModifier(string source);
    }
}