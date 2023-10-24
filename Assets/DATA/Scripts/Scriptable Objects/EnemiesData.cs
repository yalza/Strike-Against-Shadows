using UnityEngine;
using UnityEngine.Serialization;

namespace DATA.Scripts.Scriptable_Objects
{
    public class EnemiesData : ScriptableObject
    {
        [Header("Enemy Settings")]
        public LayerMask targetLayerMask;

        [FormerlySerializedAs("detectRange")] [Header("Enemy Stats")] public float fovRange;
        public float attackRange;
        public float attackDelay;
        public float attackDamage;
        public float maxHealth;
    }
}