using System;
using System.Collections;
using UnityEngine;

namespace DATA.Scripts.Object
{
    public class TimedObjectDeactiver : MonoBehaviour
    {
        public float lifeTime = 10.0f;

        private void OnEnable()
        {
            StartCoroutine(Deactive(lifeTime));
        }

        IEnumerator Deactive(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }
}