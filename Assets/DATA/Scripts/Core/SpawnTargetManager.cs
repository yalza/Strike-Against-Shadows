using System;
using UnityEngine;

namespace DATA.Scripts.Core
{
    public class SpawnTargetManager : Singleton<SpawnTargetManager>
    {
        [SerializeField] private float rangeSpawn = 10f;
        [SerializeField] public Transform player;
        [SerializeField] private float timeToSpawn = 1f;

        [SerializeField] private GameObject targetBoardPrefab;
        [SerializeField] private GameObject npcPrefab;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnTarget), 0f, timeToSpawn);
            InvokeRepeating(nameof(SpawnNpc), 0f, timeToSpawn);
        }
        
        
        private void SpawnTarget()
        {
            var position = player.position;
            float x = UnityEngine.Random.Range(position.x -rangeSpawn,position.x + rangeSpawn);
            float z = UnityEngine.Random.Range(position.z -rangeSpawn,position.z + rangeSpawn);
            
            var randomPosition = new Vector3(x,14,z);

            GameObject targetBoard = ObjectPooling.Instant.GetGameObject(targetBoardPrefab);
            targetBoard.transform.position = randomPosition;
            targetBoard.transform.LookAt(new Vector3(position.x,14,position.z));
            targetBoard.SetActive(true);
        }
        
        private void SpawnNpc()
        {
            var position = player.position;
            float x = UnityEngine.Random.Range(position.x -rangeSpawn,position.x + rangeSpawn);
            float z = UnityEngine.Random.Range(position.z -rangeSpawn,position.z + rangeSpawn);
            
            var randomPosition = new Vector3(x,-0.5f,z);

            GameObject npc = ObjectPooling.Instant.GetGameObject(npcPrefab);
            npc.transform.position = randomPosition;
            npc.transform.LookAt(new Vector3(position.x,1,position.z));
            npc.SetActive(true);
        }
        
        
    }
}
