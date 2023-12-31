using System;
using System.Collections.Generic;
using DATA.Scripts.Core;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.EnemiesAI.Tasks;
using DATA.Scripts.Interfaces;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
using Tree = DATA.Scripts.EnemiesAI.Behaviour_Tree.Tree;

namespace DATA.Scripts.EnemiesAI.Turrets
{
    public class TurretAI : Tree,IDamageable
    {
        [SerializeField] private ShootingEnemyData data;
        [SerializeField] private Transform gun;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float health;
        
        private void Start()
        {
            health = data.maxHealth;
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }
        
        protected override Node SetupTree()
        {
            var transform1 = transform;
            return root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTargetInFOVRange(transform1, data),
                    new CheckTargetInAttackRange(transform1, data),
                    new ShootingTask(gun,spawnPoint,data)
                })
            });
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
            Destroy(gameObject,0.5f);
            Instantiate(data.explosion, transform.position, Quaternion.identity);
        }
    }
}
