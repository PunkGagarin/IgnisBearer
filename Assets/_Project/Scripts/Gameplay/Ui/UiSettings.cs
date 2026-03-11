using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui
{
    // [CreateAssetMenu(fileName = "UiSettings", menuName = "Gameplay/Ui/UiSettings", order = 0)]
    public class UiSettings : ScriptableObject
    {
        [field: Header("Popup Animation")]
        [field: SerializeField] public float PopupOpenDuration { get; private set; } = 0.3f;
        [field: SerializeField] public float PopupCloseDuration { get; private set; } = 0.1f;
        [field: SerializeField] public float PopupScaleStart { get; private set; } = 0.8f;
        [field: SerializeField] public float PopupScaleOvershootStart { get; private set; } = 1.1f;
        [field: SerializeField] public float PopupCloseScale { get; private set; } = 0.5f;
        [field: SerializeField] public Ease PopupCloseEase { get; private set; } = Ease.Linear;


        [field: Header("Hover Animation")]
        [field: SerializeField] public float HoverDuration { get; private set; } = 0.2f;
        [field: SerializeField] public float HoverScale { get; private set; } = 1.1f;


        [field: Header("Generic Shake")]
        [field: SerializeField] public float ShakeRandomness { get; private set; } = 90f;
        [field: SerializeField] public float ShakeDuration { get; private set; } = 0.5f;
        [field: SerializeField] public float ShakeStrength { get; private set; } = 20f;
        [field: SerializeField] public int ShakeVibrato { get; private set; } = 10;


        [field: Header("Error Shake")]
        [field: SerializeField] public float ErrorShakeDuration { get; private set; } = 0.28f;
        [field: SerializeField] public float ErrorShakeStrength { get; private set; } = 25f;
        [field: SerializeField] public int ErrorShakeVibrato { get; private set; } = 18;


        [field: Header("Click Animation")]
        [field: SerializeField] public float ClickPressedScale { get; private set; } = 0.9f;
        [field: SerializeField] public float ClickScaleDuration { get; private set; } = 0.05f;


        [field: Header("Button States")]
        [field: SerializeField] public float EnableNormalButtonValue { get; private set; } = 1f;
        [field: SerializeField] public float EnableDisabledButtonValue { get; private set; } = 0.5f;
    }
}