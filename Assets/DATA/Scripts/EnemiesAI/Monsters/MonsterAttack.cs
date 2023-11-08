using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Monsters
{
    public class MonsterAttack : Node
    {
        private readonly Transform _transform;
        private readonly Animator _animator;
        private readonly MeleeAttackEnemyData _data;
        
        private float _attackTimer;
        
        public MonsterAttack(Transform transform , MeleeAttackEnemyData data)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _data = data;
            _attackTimer = _data.attackDelay;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");
            Transform targetTransform = (Transform)target;
            var position = targetTransform.position;
            var position1 = _transform.position;
            Vector3 targetPosition = new Vector3(position.x, position1.y, position.z);


            _animator.SetBool(_data.runningAnimationName, false);
            _transform.LookAt(targetPosition);
            _attackTimer += Time.deltaTime;
            if (_attackTimer > _data.attackDelay)
            {
                _attackTimer = 0;

                int random = Random.Range(1, 4);
                _animator.ResetTrigger(_data.attack1AnimationName);
                _animator.ResetTrigger(_data.attack2AnimationName);
                _animator.ResetTrigger(_data.attack3AnimationName);
                if (random == 1)
                {
                    _animator.SetTrigger(_data.attack1AnimationName);
                }

                if (random == 2)
                {
                    _animator.SetTrigger(_data.attack2AnimationName);
                }

                if (random == 3)
                {
                    _animator.SetTrigger(_data.attack3AnimationName);
                }
            }

            return NodeState.Running;
        }
    }
}