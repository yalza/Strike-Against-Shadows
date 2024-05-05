using System;
using DATA.Scripts.Core;
using DATA.Scripts.Interfaces;
using UnityEngine;

namespace DATA.Scripts.Object
{
    public class TargetBoard : MonoBehaviour , IDamageable
    {
        [SerializeField] private float maxHealth = 50;
        [SerializeField] private float health = 50;
        [SerializeField] private int scoreNearDistance = 1;
        [SerializeField] private int scoreFarDistance = 3;

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
            var distance = Vector3.Distance(transform.position, SpawnTargetManager.Instant.player.transform.position);
            if (distance <= 50)
            {
                GameManager.Instant.UpdateScore(1);
            }
            else
            {
                GameManager.Instant.UpdateScore(3);
            }
            gameObject.SetActive(false);
        }
    }
}
