using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
using Node = DATA.Scripts.EnemiesAI.Behaviour_Tree.Node;

namespace DATA.Scripts.EnemiesAI.Drone
{
    public class DroneGotoTarget : Node
    {
        private readonly Transform _transform;
        private readonly ShootingEnemyData _shootingEnemyData;

        public DroneGotoTarget(Transform transform, ShootingEnemyData shootingEnemyData)
        {
            _transform = transform;
            _shootingEnemyData = shootingEnemyData;
        }
        
        public override NodeState Evaluate()
        {
            object target = GetData("target");
            Transform targetTransform = (Transform)target;
            var position = targetTransform.position;
            if(Vector3.Distance(_transform.position, position) > 0.01f)
            {
                _transform.LookAt(position);
                _transform.position = Vector3.MoveTowards(_transform.position, position, _shootingEnemyData.moveSpeed * Time.deltaTime);
            }
            return NodeState.Running;
        }
        
    }
}