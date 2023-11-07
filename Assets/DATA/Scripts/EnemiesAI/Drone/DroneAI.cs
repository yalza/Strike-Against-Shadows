using System;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Interfaces;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
using Tree = DATA.Scripts.EnemiesAI.Behaviour_Tree.Tree;

namespace DATA.Scripts.EnemiesAI.Drone
{
    public class DroneAI : Tree , IDamageable
    {
        public DroneData droneData;
        public float health;
        private void Start()
        {
            root = SetupTree();
            health = droneData.maxHealth;
        }

        private void Update()
        {
            if(root!=null)
                root.Evaluate();
            
            
        }

        protected override Node SetupTree()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
