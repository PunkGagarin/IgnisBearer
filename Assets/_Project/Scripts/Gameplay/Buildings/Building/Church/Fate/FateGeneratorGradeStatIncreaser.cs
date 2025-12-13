using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class FateGeneratorGradeStatIncreaser : MonoBehaviour
    {
        private Grade _grade;

        private void Awake()
        {
            //1
            _grade = GetComponentInParent<Grade>();
        }

        //2
        public void Init(Grade grade)
        {
            _grade = grade;
        }
    }
}