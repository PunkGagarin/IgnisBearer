using System;
using _Project.Scripts.Gameplay.Ui;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchLightSendSlot : MonoBehaviour
    {
        [field: SerializeField]
        public BarUi BarUi { get; private set; }


        public bool IsActive { get; private set; } = true;
        public bool IsBusy { get; private set; }
        public Action<ChurchLightSendSlot> OnFree = delegate { };

        private void Awake()
        {
            ResetProgress();
        }

        private void ResetProgress()
        {
            Progress(0f);
        }

        public void SetBusy()
        {
            IsBusy = true;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public void Progress(float progress)
        {
            BarUi.ChangeBarProgress(progress);
        }

        public void SetFree()
        {
            IsBusy = false;
            ResetProgress();
            OnFree.Invoke(this);
        }
    }
}