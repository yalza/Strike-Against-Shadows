using UnityEngine;

namespace DATA.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Turret Data", menuName = "ScriptableObjects/Enemy Data", order = 1)]
    public class ShootingEnemyData : EnemiesData
    {
        [Header("Generals Settings")]
        public GameObject projectile;
        public GameObject explosion;
        public GameObject muzzleVfx;
        
    }
}
