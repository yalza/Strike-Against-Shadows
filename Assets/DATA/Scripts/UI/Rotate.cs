using DG.Tweening;
using UnityEngine;

namespace DATA.Scripts.UI
{
    public class Rotate : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
    }
}