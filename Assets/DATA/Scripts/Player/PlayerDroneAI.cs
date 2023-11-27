using System.Collections.Generic;
using DATA.Scripts.Core;
using DATA.Scripts.EnemiesAI.Behaviour_Tree;
using DATA.Scripts.EnemiesAI.Tasks;
using DATA.Scripts.Interfaces;
using DATA.Scripts.Scriptable_Objects;
using UnityEngine;
using Tree = DATA.Scripts.EnemiesAI.Behaviour_Tree.Tree;

namespace DATA.Scripts.Player
{
    public class PlayerDroneAI : MonoBehaviour
    {
        [SerializeField] private ShootingEnemyData playerDroneData;
        [SerializeField] private Transform spawnPoint;


        private void Update()
        {
            var muzzles = spawnPoint.GetComponentsInChildren<Transform>();

            if (muzzles.Length == 1)
            {

                var position = spawnPoint.position;
                var rotation = spawnPoint.rotation;
                Spawn(playerDroneData.projectile, position, rotation);
                Spawn(playerDroneData.muzzleVfx, position, rotation);
            }
            else
            {
                for (int i = 1; i < muzzles.Length; i++)
                {
                    var position = muzzles[i].position;
                    var rotation = muzzles[i].rotation;
                    Spawn(playerDroneData.projectile, position, rotation);
                    Spawn(playerDroneData.muzzleVfx, position, rotation);
                }
            }

        }

        private void Spawn(GameObject obj, Vector3 position, Quaternion rotation)
        {
            
            GameObject tmpGameObject = ObjectPooling.Instant.GetGameObject(obj);
            tmpGameObject.transform.position = position;
            tmpGameObject.transform.rotation = rotation;
            tmpGameObject.SetActive(true);
        }
       
    }
}