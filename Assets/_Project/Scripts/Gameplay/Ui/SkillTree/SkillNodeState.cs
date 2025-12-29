namespace _Project.Scripts.Gameplay.Ui.SkillTree
{
    public enum SkillNodeState
    {
        None = 0,
        Unreachable = 1,
        CanBuy = 2,
        NoMoney = 3,
    }

    public enum NodeBoughtState
    {
        None = 0,
        NotBought = 1,
        Bought = 2,
        Maxed = 3
        
    }
}