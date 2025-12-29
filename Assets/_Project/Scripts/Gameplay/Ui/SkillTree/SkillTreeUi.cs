using System;
using System.Collections.Generic;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.SkillTree
{
    public class SkillTreeUi : ContentUi
    {
        [field: SerializeField]
        public Button OpenButton { get; private set; }

        [field: SerializeField]
        public List<Button> CloseButtons { get; private set; }

        [field: SerializeField]
        public List<SkillNodeUI> SkillNodeUIs { get; private set; }
        
        public event Action<SkillNodeUI> OnNodeClicked = delegate { };

        private void Awake()
        {
            OpenButton.onClick.AddListener(Show);
            foreach (var closeButton in CloseButtons)
                closeButton.onClick.AddListener(Hide);

            foreach (var node in SkillNodeUIs)
            {
                node.OnClick += OnNodeClickedHandle;
            }
        }

        private void OnDestroy()
        {
            OpenButton.onClick.RemoveListener(Show);
            foreach (var closeButton in CloseButtons)
                closeButton.onClick.RemoveListener(Hide);

            foreach (var node in SkillNodeUIs)
                node.OnClick -= OnNodeClickedHandle;
        }

        private void OnNodeClickedHandle(SkillNodeUI node)
        {
            OnNodeClicked.Invoke(node);
        }
    }
}