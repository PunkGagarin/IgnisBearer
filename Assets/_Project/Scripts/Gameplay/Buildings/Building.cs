using _Project.Scripts.Gameplay.BuildingComponents.Durability;
using _Project.Scripts.Gameplay.BuildingComponents.Grade;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay
{
    public abstract class Building : MonoBehaviour
    {
        [SerializeField] private Button _button;
        protected IGrade _grade;
        protected IDurability _durability;

        private void Start()
        {
            _button.onClick.AddListener(HandleButtonClick);
            _grade = GetComponent<IGrade>();
            _durability = GetComponent<IDurability>();
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