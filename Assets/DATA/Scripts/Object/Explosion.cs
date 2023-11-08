using System;
using System.Collections;
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
        public LayerMask targetlayerMask;
        
        private void Start()
        {
            Explode();
        }


        private void Explode()
        {
            Collider[] results = new Collider[100];
            var size = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, results , targetlayerMask);
            for (int i = 0 ; i < size ;i++)
            {
                Vibration vibration = results[i].GetComponentInChildren<Vibration>();
                if (vibration != null)
                {
                    float shakeVoilence = 1 / Vector3.Distance(transform.position, results[i].transform.position);
                    vibration.StartShakingRandom(-shakeVoilence,shakeVoilence,-shakeVoilence,shakeVoilence);
                }

                IDamageable damageable = results[i].GetComponent<IDamageable>();
                if (causeDamage && damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}