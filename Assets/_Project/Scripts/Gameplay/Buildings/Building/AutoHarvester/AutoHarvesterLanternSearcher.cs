using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Lanterns;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoHarvesterLanternSearcher : BaseLanternSearcher
    {
        protected override void SubscribeOnLantern()
        {
            // _lanternService.OnLanternFull += SendFirstUnit;
        }

        protected override List<Lantern> GetLanternsForQueue()
        {
            return _lanternService.GetUnharvestedLanterns();
        }

        protected override void UnsubscribeFromLantern()
        {
            // _lanternService.OnLanternFull -= SendFirstUnit;
        }
    }
}