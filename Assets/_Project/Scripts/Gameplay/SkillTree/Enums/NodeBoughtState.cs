using System;

namespace _Project.Scripts.Gameplay.SkillTree
{
    [Flags]
    public enum NodeBoughtState
    {
        None = 0,
        NotBought = 1,
        Bought = 2,
        Maxed = 4
    }
}