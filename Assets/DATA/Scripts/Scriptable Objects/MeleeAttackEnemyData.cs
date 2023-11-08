using UnityEngine;
using UnityEngine.Serialization;

namespace DATA.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Monster Data", menuName = "ScriptableObjects/Monster Data", order = 2)]
    public class MeleeAttackEnemyData : EnemiesData
    {
        
        [Header("Animations name")]
        public string runningAnimationName = "Running";
        public string attack1AnimationName = "Attack1";
        public string attack2AnimationName = "Attack2";
        public string attack3AnimationName = "Attack3";
        public string dieAnimationName = "Die";

    }
}