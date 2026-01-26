namespace _Project.Scripts.Gameplay
{
    public interface IStat<ST>
    {
        public ST Type { get; }
        public float GetValue();
        public int GetRoundedValue();
        public void AddModifier(StatModifier mod);
        public void RemoveModifier(string source);
    }
}