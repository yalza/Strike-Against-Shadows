using System;
using System.Collections.Generic;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using UnityEngine;
using Tree = DATA.Scripts.EnemiesAI.Behaviour_Tree.Tree;

namespace DATA.Scripts.EnemiesAI.Turrets
{
    public class TurretAI : Tree
    {
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask playerLayerMask;

        private void Start()
        {
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
            var root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckEnemyInAttackRange(transform1, attackRange,playerLayerMask),
                    new TurretTaskAttack(transform1)
                })
            });
            return root;
        }
    }
}
