using System;
using DATA.Scripts.Core;
using DATA.Scripts.Interfaces;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace DATA.Scripts.Weapon
{
    public class KatanaAttack: MonoBehaviour
    {
        public float damage;
        
        private void Awake()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}