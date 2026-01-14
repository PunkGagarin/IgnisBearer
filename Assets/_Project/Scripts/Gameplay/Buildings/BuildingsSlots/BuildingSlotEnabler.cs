using System;
using _Project.Scripts.Gameplay.Tutorial;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class BuildingSlotEnabler : IDisposable
    {
        private int _currentRes;
        private int _targetRes;

        [Inject] private BuildingsService _buildingsService;
        [Inject] private BuildingSlotsService _buildingSlotsService;
        [Inject] private TutorialSettings _tutorialSettings;

        public void Init()
        {
            var church = _buildingsService.GetChurch();
            
            //todo: change to BarrierService or smth like that which should be indepandable from church maybe??
            var lightResourceStorage = church.GetComponent<IResourceStorage>();
            lightResourceStorage.OnAmountIncreased += Iterate;

            _targetRes = _tutorialSettings.LightCountForChurch;
        }

        private void Iterate((int amountIncreased, int newAmount, int maxAmount) valueTuple)
        {
            _currentRes++;
            if (_currentRes >= _targetRes)
                _buildingSlotsService.EnableButtonForSlots();
        }


        public void Dispose()
        {
            var church = _buildingsService.GetChurch();
            var lightResourceStorage = church.GetComponent<IResourceStorage>();
            lightResourceStorage.OnAmountIncreased -= Iterate;
        }
    }
}