using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Turrets
{
    public class TurretTaskAttack : Node
    {
        private readonly Transform _transform;
        
        public TurretTaskAttack(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform) GetData("target");
            _transform.LookAt(target.position);
            return NodeState.Running;
        }
    }
}
