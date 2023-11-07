using UnityEngine;

namespace DATA.Scripts.Object
{
    public class TimedObjectDestroyer : MonoBehaviour
    {
        public float lifeTime = 10.0f;
        
        void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}