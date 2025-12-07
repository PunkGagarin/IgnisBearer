using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [field: SerializeField] public BuildingType Type { get; private set; }

        [SerializeField] private Button _button;

        protected IGrade _grade;

        protected virtual void Awake()
        {
            _grade = GetComponent<IGrade>();
        }

        private void Start()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDestroy() => _button.onClick.RemoveListener(HandleButtonClick);

        protected virtual void HandleButtonClick()
        {
            // no-op
        }

        public bool UpdateGrade()
        {
            return _grade.UpdateGrade();
            // todo
        }
    }
}