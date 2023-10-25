using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Tasks
{
    public class CheckTargetInFOVRange : Node
    {
        private readonly Transform _transform;
        private readonly EnemiesData _data;

        public CheckTargetInFOVRange(Transform transform, EnemiesData data)
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
                
                var size = Physics.OverlapSphereNonAlloc(_transform.position,_data.fovRange,colliders,_data.targetLayerMask);
                if (size > 0)
                {
                    parent.parent.SetData("target",colliders[0].transform);
                    return NodeState.Success;
                }
                
                return NodeState.Failure;
            }
            

            return NodeState.Success;
        }
    }
}