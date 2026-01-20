using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class MoneyGdCheat : MonoBehaviour
    {
        [Inject] private BuildingsService _buildingsService;
        [field: SerializeField] private int _moneyCount = 10000;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && Debug.isDebugBuild)
            {
                var fateGeneratorBuilding = _buildingsService.GetBuilding<FateGeneratorBuilding>();
                if (fateGeneratorBuilding == null)
                {
                    Debug.Log("Chapel is not created yet");
                    return;
                }

                var fateStorage = fateGeneratorBuilding.GetComponent<ResourceStorage>();
                fateStorage.IncrementAmount(_moneyCount);
            }
        }
    }
}