using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
namespace DATA.Scripts.EnemiesAI.Monsters
{
    public class MonsterPatrol : Node
    {
        private readonly Transform[] _waypoints;
        private readonly Transform _transform;
        private readonly Animator _animator;
        private readonly MonsterData _data;
        
        private int _currentWaypointIndex;
        private float _waitTime = 1f;
        private float _waitTimer;
        private bool _isWaiting;
        

        public MonsterPatrol(Transform transform,MonsterData data, Transform[] waypoints)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _waypoints = waypoints;
            _data = data;
        }

        public override NodeState Evaluate()
        {
            if (_isWaiting)
            {
                _animator.SetBool(_data.runningAnimationName,false);
                _waitTimer += Time.deltaTime;
                if(_waitTimer>= _waitTime)
                {
                    _isWaiting = false;
                    _waitTimer = 0f;
                }
            }
            else
            {
                Vector3 waypoint = _waypoints[_currentWaypointIndex].position;
                Vector3 targetPosition = new Vector3(waypoint.x, _transform.position.y, waypoint.z);
                if (Vector3.Distance(_transform.position, targetPosition) < 0.1f)
                {
                    _isWaiting = true;
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                    _waitTimer = 0;
                }
                else
                {
                    _transform.LookAt(targetPosition);
                    _animator.SetBool(_data.runningAnimationName,true);
                    _transform.position = Vector3.MoveTowards(_transform.position,targetPosition,_data.moveSpeed * Time.deltaTime);
                    
                }
            }
            return NodeState.Running;
        }
        
        
    }
}