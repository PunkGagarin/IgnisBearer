using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using UnityEngine;
using Zenject;

public class MoneyGd : MonoBehaviour
{
    [Inject] private BuildingsService _buildingsService;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var a = _buildingsService.GetBuilding<FateGeneratorBuilding>().GetComponent<ResourceStorage>();
            a.IncrementAmount(10000);
        }
    }
}