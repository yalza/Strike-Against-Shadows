using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Tasks
{
    public class CheckTargetInAttackRange : Node
    {
        private readonly Transform _transform;
        private readonly EnemiesData _data;
        
        public CheckTargetInAttackRange(Transform transform, EnemiesData data)
        {
            _transform = transform;
            _data = data;
        }
        
        public override NodeState Evaluate()
        {
            object target = GetData("target");
            if(target == null)
                return NodeState.Failure;
            Transform targetTransform = (Transform) target;
            if(Vector3.Distance(_transform.position, targetTransform.position) <= _data.attackRange)
                return NodeState.Success;
            return NodeState.Failure;
        }
        
    }
}
