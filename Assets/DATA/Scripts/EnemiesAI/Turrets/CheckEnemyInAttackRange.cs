using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Turrets
{
    public class CheckEnemyInAttackRange : Node
    {
        private readonly Transform _transform;
        private readonly float _attackRange;
        private readonly LayerMask _targetLayerMask;
        
        public CheckEnemyInAttackRange(Transform transform, float attackRange,LayerMask layerMask)
        {
            _transform = transform;
            _attackRange = attackRange;
            _targetLayerMask = layerMask;
        }
        
        public override NodeState Evaluate()
        {
            object target = GetData("target");
            if (target == null)
            {
                Collider[] colliders = new Collider[5];
                var size = Physics.OverlapSphereNonAlloc(_transform.position, _attackRange, colliders,_targetLayerMask);
                if (size > 0)
                {
                    parent.parent.SetData("target",colliders[0].transform);
                    return NodeState.Success;
                }
                return NodeState.Failure;
            }
            
            Transform targetTransform = (Transform) target;
            if(Vector3.Distance(_transform.position, targetTransform.position) <= _attackRange)
                return NodeState.Success;
            return NodeState.Failure;
        }
        
    }
}
