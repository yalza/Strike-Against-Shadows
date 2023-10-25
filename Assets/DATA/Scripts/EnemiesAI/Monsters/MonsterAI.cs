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
            var transform1 = transform;
            return root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTargetInAttackRange(transform1,data),
                    new MonsterAttack(transform1,data)
                }),
                new Sequence(new List<Node>
                {
                    new CheckTargetInFOVRange(transform1,data),
                    new GotoTarget(transform1,data),
                }),
                new MonsterPatrol(transform1,data,waypoints),
            });
        }

        public void TakeDamage(float damage)
        {
        }
    }
}
