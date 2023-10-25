using System;
using System.Collections.Generic;
using DATA.Scripts.Core;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.EnemiesAI.Tasks;
using DATA.Scripts.Interfaces;
using DATA.Scripts.Player;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
using Tree = DATA.Scripts.EnemiesAI.Behaviour_Tree.Tree;

namespace DATA.Scripts.EnemiesAI.Monsters
{
    public class MonsterAI : Tree,IDamageable
    {
        public MonsterData data;
        public Transform[] waypoints;
        
        private void Start()
        {
            root = SetupTree();
        }

        private void Update()
        {
            if(root!=null)
                root.Evaluate();
        }


        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                /*new Sequence(new List<Node>
                {
                    
                }),*/
                new MonsterPatrol(transform,data,waypoints),
            });

            return root;
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
