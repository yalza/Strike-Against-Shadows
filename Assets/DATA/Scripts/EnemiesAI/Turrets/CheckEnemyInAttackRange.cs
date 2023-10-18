using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Turrets
{
    public class CheckEnemyInAttackRange : Node
    {
        private readonly Transform _transform;
        private readonly EnemyData _data;
        
        public CheckEnemyInAttackRange(Transform transform, EnemyData data)
        {
            _transform = transform;
            _data = data;
        }
        
        public override NodeState Evaluate()
        {
            object target = GetData("target");
            if (target == null)
            {
                Collider[] colliders = new Collider[5];
                var size = Physics.OverlapSphereNonAlloc(_transform.position, -_data.attackRange, colliders,_data.targetLayerMask);
                if (size > 0)
                {
                    parent.parent.SetData("target",colliders[0].transform);
                    return NodeState.Success;
                }
                return NodeState.Failure;
            }
            
            Transform targetTransform = (Transform) target;
            if(Vector3.Distance(_transform.position, targetTransform.position) <= _data.attackRange)
                return NodeState.Success;
            return NodeState.Failure;
        }
        
    }
}
