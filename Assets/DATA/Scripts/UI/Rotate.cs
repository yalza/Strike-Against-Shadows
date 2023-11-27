using DG.Tweening;
using UnityEngine;

namespace DATA.Scripts.UI
{
    public class Rotate : MonoBehaviour
    {
        public float speed;

        public Vector3 direction = Vector3.up;

        private void Update()
        {
            transform.Rotate(direction, speed * Time.deltaTime);
        }
    }
}