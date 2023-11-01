using System;
using DATA.Scripts.Core;
using DATA.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace DATA.Scripts.Object
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float damage = 1f;
        [SerializeField] private GameObject impactEffect;
        

        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rigidbody.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            GameObject impact = ObjectPooling.Instant.GetGameObject(impactEffect);
            var transform1 = transform;
            impact.transform.position = transform1.position;
            impact.transform.rotation = transform1.rotation;
            impact.SetActive(true);
            ObjectManager.Instant.StartDelayDeactive(2f,impact);
            
            gameObject.SetActive(false);
            
        }
    }
}
