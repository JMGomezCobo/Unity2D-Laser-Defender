using System.Collections.Generic;
using UnityEngine;

namespace LaserDefender.Data
{
    [CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")] 
    public class EnemyWaveData : ScriptableObject
    {
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private Transform pathPrefab;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float timeBetweenEnemySpawns = 1f;
        [SerializeField] private float spawnTimeVariance = 0f;
        [SerializeField] private float minimumSpawnTime = 0.2f;

        public int GetEnemyCount()
        {
            return enemyPrefabs.Count;
        }

        public GameObject GetEnemyPrefab(int index)
        {
            return enemyPrefabs[index];
        }

        public Transform GetStartingWaypoint()
        {
            return pathPrefab.GetChild(0);
        }

        public List<Transform> GetWaypoints()
        {
            var waypoints = new List<Transform>();
            foreach(Transform child in pathPrefab)
            {
                waypoints.Add(child);
            }
            return waypoints;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        public float GetRandomSpawnTime()
        {
            var spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
                timeBetweenEnemySpawns + spawnTimeVariance);
            return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
        }
    }
}
