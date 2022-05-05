using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaserDefender.AI
{
    [CreateAssetMenu(menuName = "Laser Defender/AI/Wave Data", fileName = "New Wave Data")] 
    public class AIWaveData : ScriptableObject
    {
        [Header("Enemy Prefabs")]
        [SerializeField] private List<GameObject> _enemyPrefabs;
        [Space]
        [SerializeField] private Transform _pathPrefab;
        
        [Header("Wave Settings")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _timeBetweenEnemySpawns = 1f;
        [SerializeField] private float _spawnTimeVariance;
        [SerializeField] private float _minimumSpawnTime = 0.2f;

        public GameObject GetEnemyPrefab(int index) => _enemyPrefabs[index];
        
        public Transform GetStartingWaypoint() => _pathPrefab.GetChild(0);

        public List<Transform> GetWaypoints() => _pathPrefab.Cast<Transform>().ToList();
        
        public float GetMoveSpeed() => _moveSpeed;

        public int GetEnemyCount() => _enemyPrefabs.Count;
        
        public float GetRandomSpawnTime()
        {
            var spawnTime = Random.Range(_timeBetweenEnemySpawns - _spawnTimeVariance, _timeBetweenEnemySpawns + _spawnTimeVariance);
            
            return Mathf.Clamp(spawnTime, _minimumSpawnTime, float.MaxValue);
        }
    }
}