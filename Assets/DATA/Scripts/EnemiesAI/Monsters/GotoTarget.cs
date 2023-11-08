using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Monsters
{
    public class GotoTarget : Node
    {
        private readonly Transform _transform;
        private readonly Animator _animator;
        private readonly MeleeAttackEnemyData _data;

        public GotoTarget(Transform transform, MeleeAttackEnemyData meleeAttackEnemyData)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _data = meleeAttackEnemyData;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");
            Transform targetTransform = (Transform)target;
            var position = targetTransform.position;
            var position1 = _transform.position;
            Vector3 targetPosition = new Vector3(position.x, position1.y, position.z);
            if(Vector3.Distance(_transform.position, targetPosition) > 0.01f)
            {
                _transform.LookAt(targetPosition);
                position1 = Vector3.MoveTowards(position1, targetPosition, _data.moveSpeed * Time.deltaTime);
                _transform.position = position1;
                /*_animator.ResetTrigger(_data.attack1AnimationName);
                _animator.ResetTrigger(_data.attack2AnimationName);
                _animator.ResetTrigger(_data.attack3AnimationName);*/
                _animator.SetBool(_data.runningAnimationName, true);
            }
            return NodeState.Running;
        }
    }
}