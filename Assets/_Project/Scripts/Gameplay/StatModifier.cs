namespace _Project.Scripts.Gameplay
{
    public class StatModifier
    {
        public ModifierType Type { get; private set; }
        public float Value { get; private set; }
        public string Source { get; private set; }

        public StatModifier(ModifierType type, float value, string source)
        {
            Type = type;
            Value = value;
            Source = source;
        }
    }

    public enum ModifierType
    {
        Sum = 1,
        Multiply = 2,
    }
}