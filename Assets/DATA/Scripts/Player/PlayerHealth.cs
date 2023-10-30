using System;
using System.Collections.Generic;
using DATA.Scripts.Core;
using DATA.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace DATA.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour,IDamageable
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth = 100f;
        

        private void Start()
        {
            health = maxHealth;
            Observer.Instant.NotifyObservers(Constant.updateHpSlider,new List<object>{health,maxHealth});
        }

        public void TakeDamage(float damage)
        {
            Observer.Instant.NotifyObservers(Constant.updateHpSlider,new List<object>{health,maxHealth});
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