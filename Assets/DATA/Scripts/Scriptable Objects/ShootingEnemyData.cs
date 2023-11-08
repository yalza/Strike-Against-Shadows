using UnityEngine;

namespace DATA.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "ShootingEnemyData Data", menuName = "ScriptableObjects/ShootingEnemyData Data", order = 1)]
    public class ShootingEnemyData : EnemiesData
    {
        [Header("Generals Settings")]
        public GameObject projectile;
        public GameObject explosion;
        public GameObject muzzleVfx;
        
    }
}
