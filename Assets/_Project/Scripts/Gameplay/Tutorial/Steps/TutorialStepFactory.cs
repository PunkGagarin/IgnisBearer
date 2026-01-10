using System;
using Zenject;

namespace _Project.Scripts.Gameplay.Tutorial
{
    public class TutorialStepFactory
    {
        [Inject] private DiContainer _container;

        public ITutorialStep CreateStep(TutorStepType type)
        {
            switch (type)
            {
                case TutorStepType.BuyUnit:
                    return _container.Instantiate<BuyUnitStep>();
                case TutorStepType.FireUpLantern:
                    return _container.Instantiate<FireUpLanternStep>();
                case TutorStepType.SendLightToChurch:
                    return _container.Instantiate<SendLightToChurchStep>();
                case TutorStepType.BuyChapel:
                    return _container.Instantiate<BuyChapelStep>();
                case TutorStepType.SendUnitIntoChapel:
                    return _container.Instantiate<SendUnitIntoChapelStep>();
                case TutorStepType.BuySecondUnit:
                    return _container.Instantiate<BuySecondUnitStep>();
                default:
                    throw new ArgumentException();
            }
        }
    }
}