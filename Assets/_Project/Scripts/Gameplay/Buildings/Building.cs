using System;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.Durability;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.Grade;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [field: SerializeField] public BuildingType Type { get; private set; }

        [SerializeField] private Button _button;

        protected IGrade _grade;
        protected IDurability _durability;

        protected virtual void Awake()
        {
            _grade = GetComponent<IGrade>();
            _durability = GetComponent<IDurability>();
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