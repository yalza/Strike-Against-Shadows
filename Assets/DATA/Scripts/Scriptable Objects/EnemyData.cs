using UnityEngine;

namespace DATA.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy Data", order = 1)]
    public class EnemyData : ScriptableObject
    {
        [Header("Generals Settings")]
        public GameObject projectile;
        public GameObject explosion;
        public GameObject muzzleVfx;
        
        [Header("Enemy Settings")]
        public LayerMask targetLayerMask;
        
        [Header("Enemy Stats")]
        public float attackRange;
        public float attackDelay;
        public float attackDamage;
        public float maxHealth;
    }
}
