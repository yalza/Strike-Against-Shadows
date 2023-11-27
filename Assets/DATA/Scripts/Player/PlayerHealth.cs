using DATA.Scripts.Core;
using DATA.Scripts.Interfaces;
using UnityEngine;

namespace DATA.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour,IDamageable
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth = 100f;
        

        private void Start()
        {
            health = maxHealth;
            Observer.Instant.NotifyObservers(Constant.updateHpSlider,(health,maxHealth));
        }

        public void TakeDamage(float damage)
        {
            Observer.Instant.NotifyObservers(Constant.updateHpSlider,(health,maxHealth));
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
        
        public void Die()
        {
            Destroy(gameObject);
        }
        
        
    }
}