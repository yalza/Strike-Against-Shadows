using System;
using System.Collections.Generic;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.EnemiesAI.Tasks;
using DATA.Scripts.Interfaces;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
using UnityEngine.Serialization;
using Tree = DATA.Scripts.EnemiesAI.Behaviour_Tree.Tree;

namespace DATA.Scripts.EnemiesAI.Drone
{
    public class DroneAI : Tree , IDamageable
    {
        [SerializeField] private ShootingEnemyData shootingEnemyData;
        [SerializeField] private Transform muzzle;
        public float health;
        private void Start()
        {
            root = SetupTree();
            health = shootingEnemyData.maxHealth;
        }

        private void Update()
        {
            if(root!=null)
                root.Evaluate();
        }

        protected override Node SetupTree()
        {
            var transform1 = transform;
            return root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTargetInAttackRange(transform1,shootingEnemyData),
                    new ShootingTask(transform1,muzzle,shootingEnemyData)
                }),
                new Sequence(new List<Node>
                {
                    new CheckTargetInFOVRange(transform1,shootingEnemyData),
                    new DroneGotoTarget(transform1,shootingEnemyData)
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
            Instantiate(shootingEnemyData.explosion, transform.position + Vector3.down * 2, Quaternion.identity);
        }
    }
}
