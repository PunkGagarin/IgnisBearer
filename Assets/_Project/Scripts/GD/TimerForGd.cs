using TMPro;
using UnityEngine;

namespace _Project.Scripts.GD
{
    public class TimerForGd : MonoBehaviour
    {

        private TextMeshProUGUI _text;
    
        private float _time;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _time += Time.deltaTime;
            _text.text = _time.ToString("F2");
        }
    }
}
