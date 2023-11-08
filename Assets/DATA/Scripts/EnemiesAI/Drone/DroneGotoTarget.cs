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
            var position1 = _transform.position;
            Vector3 targetPosition = new Vector3(position.x, position1.y, position.z);
            if(Vector3.Distance(_transform.position, targetPosition) > 0.01f)
            {
                _transform.LookAt(targetPosition);
                position1 = Vector3.MoveTowards(position1, targetPosition, _shootingEnemyData.moveSpeed * Time.deltaTime);
                _transform.position = position1;
            }
            return NodeState.Running;
        }
        
    }
}