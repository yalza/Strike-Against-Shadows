using DATA.Scripts.Core;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Interfaces;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
using Tree = DATA.Scripts.EnemiesAI.Behaviour_Tree.Tree;

namespace DATA.Scripts.EnemiesAI.Monsters
{
    public class MonsterAI : Tree,IDamageable
    {
        [SerializeField] private MonsterData monsterData;


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
