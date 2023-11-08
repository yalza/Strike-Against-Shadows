using DATA.Scripts.Core;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Tasks
{
    public class ShootingTask : Node
    {
        private readonly Transform _spawnPoint;
        private readonly Transform _transformGun;
        private readonly ShootingEnemyData _data;
        private float _timmer = 0;
        
        public ShootingTask(Transform transform,Transform spawnPoint, ShootingEnemyData data)
        {
            _spawnPoint = spawnPoint;
            _transformGun = transform;
            _data = data;
        }

        public override NodeState Evaluate()
        {
            
            _timmer += Time.deltaTime;
            if (_timmer >= _data.attackDelay)
            {
                _timmer = 0;
                Attack();
            }
            
            return NodeState.Running;
        }

        private void Attack()
        {
            Transform target = (Transform) GetData("target");
            var position1 = target.position;
            _transformGun.LookAt(new Vector3(position1.x,_transformGun.position.y,position1.z));

            var position = _spawnPoint.position;
            var rotation = _spawnPoint.rotation;
            Spawn(_data.projectile,position,rotation,2f);
            Spawn(_data.muzzleVfx,position,rotation,0.5f);
            
        }

        private void Spawn(GameObject obj, Vector3 position, Quaternion rotation,float lifeTime)
        {
            GameObject tmpGameObject = ObjectPooling.Instant.GetGameObject(obj);
            tmpGameObject.transform.position = position;
            tmpGameObject.transform.rotation = rotation;
            tmpGameObject.SetActive(true);
            ObjectManager.Instant.StartDelayDeactive(lifeTime,tmpGameObject);
        }
        
    }
}
