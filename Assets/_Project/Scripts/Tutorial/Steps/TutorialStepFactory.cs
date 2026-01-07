using _Project.Scripts.Tutorial.Steps.ConcreteSteps;
using Zenject;

namespace _Project.Scripts.Tutorial
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
                default:
                    return null;
            }
        }
    }
}