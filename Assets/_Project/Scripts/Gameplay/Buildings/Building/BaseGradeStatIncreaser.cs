using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(IGrade))]
    public abstract class BaseGradeStatIncreaser : MonoBehaviour
    {
        protected Grade _grade;

        private void Awake()
        {
            _grade = GetComponent<Grade>();
            _grade.OnGradeChanged += OnGradeChanged;
        }

        private void OnDestroy()
        {
            _grade.OnGradeChanged -= OnGradeChanged;
        }

        protected abstract void OnGradeChanged(int newGrade);
        
        protected void UpdateResourceStorage(GameObject building, int maxStorageCapacity)
        {
            building.TryGetComponent<IResourceStorage>(out var storage);
            storage.Init(maxStorageCapacity);
        }

        protected void UpdateGrade(GameObject building, int gradePrice)
        {
            building.TryGetComponent<IGrade>(out var grade);
            grade.SetNextGradePrice(gradePrice);
        }

        protected void UpdateResourceProducer(GameObject building, float timeToProduceFate)
        {
            var fateProducer = building.GetComponent<ResourceProducer>();
            fateProducer.Init(timeToProduceFate);
        }

        protected void UpdateWorkers(GameObject building, int maxUnitsCount)
        {
            building.TryGetComponent<IWorkers>(out var workers);
            workers.Init(maxUnitsCount);
        }

        protected void UpdateDurability(GameObject building, int maxDurability)
        {
            building.TryGetComponent<IDurability>(out var durability);
            durability.SetMaxValue(maxDurability);
        }
    }
}