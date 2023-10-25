using UnityEngine;
using UnityEngine.Serialization;

namespace DATA.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Monster Data", menuName = "ScriptableObjects/Monster Data", order = 2)]
    public class MonsterData : EnemiesData
    {
        
        [Header("Animations name")]
        public string runningAnimationName = "Running";
        public string attack1AnimationName = "Attack1";
        public string attack2AnimationName = "Attack2";
        public string attack3AnimationName = "Attack3";
        public string dieAnimationName = "Die";

        [Header("Monster stats")] public float moveSpeed;

    }
}