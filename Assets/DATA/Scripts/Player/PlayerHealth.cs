using System;
using DATA.Scripts.Core;
using DATA.Scripts.Interfaces;
using UnityEngine;

namespace DATA.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour,IDamageable
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth = 1000f;

        private void Start()
        {
            health = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
        
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}