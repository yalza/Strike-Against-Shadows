using DATA.Scripts.Core;
using DATA.Scripts.Interfaces;
using UnityEngine;

namespace DATA.Scripts.Object
{
    public class HeadNpc : MonoBehaviour , IDamageable
    {
        [SerializeField] private float maxHealth = 1;
        [SerializeField] private float health = 1;

        private int _score = 8;

        private void Start()
        {
            health = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GameManager.Instant.UpdateScore(_score);
            var transform1 = transform;
            Transform parent = transform1.parent;
            Transform p = transform1;
            while(parent != null)
            {
                p = parent;
                parent = parent.parent;
            }
            p.gameObject.SetActive(false);        }
    }
}
