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
            Transform parent = other.transform.parent;
            IDamageable damageable = null;
            while (parent != null)
            {
                damageable = parent.GetComponent<IDamageable>();
                parent = parent.parent;
            }
            if(damageable!= null)
                damageable.TakeDamage(damage);
        }
    }
}