using System;
using System.Collections.Generic;
using DATA.Scripts.Interfaces;
using DATA.Scripts.Player;
using UnityEngine;

namespace DATA.Scripts.Object
{
    public class Explosion : MonoBehaviour
    {
        public float explosionForce = 5f;
        public float explosionRadius = 10f;
        public bool causeDamage = true;
        public float damage  = 10f;
        
        private void Start()
        {
            var results = new Collider[100];
            var size = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, results);
            
            foreach (var col in results)
            {
                Vibration vibration = col.GetComponent<Vibration>();
                if (vibration != null)
                {
                    float shakeVoilence = 1 / Vector3.Distance(transform.position, col.transform.position);
                    vibration.StartShakingRandom(-shakeVoilence,shakeVoilence,-shakeVoilence,shakeVoilence);
                }

                IDamageable damageable = col.GetComponent<IDamageable>();
                if (causeDamage && damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
                
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce,transform.position,explosionRadius,1,ForceMode.Impulse);
                }
            }
        }
    }
}