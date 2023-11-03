using UnityEngine;

namespace DATA.Scripts.UI
{
    public class MinimapCamera : MonoBehaviour
    {
        public Transform target;
        void Start()
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            var position = target.position;
            var transform1 = transform;
            transform1.position = new Vector3(position.x, transform1.position.y, position.z);
        }
    }
}
