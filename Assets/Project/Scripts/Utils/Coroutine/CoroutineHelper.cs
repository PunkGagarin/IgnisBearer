using System.Collections;
using UnityEngine;

namespace Project.Scripts.Utils.Coroutine
{
    public class CoroutineHelper : MonoBehaviour
    {
        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
