using UnityEngine;

namespace DATA.Scripts.Scriptable_Objects
{
    public class EnemiesData : ScriptableObject
    {
        public string enemyName;
        
        [Header("Enemy Settings")]
        public LayerMask targetLayerMask;

        [Header("Enemy Stats")] public float fovRange;
        public float attackRange;
        public float attackDelay;
        public float attackDamage;
        public float maxHealth;
        public float moveSpeed;
    }
}