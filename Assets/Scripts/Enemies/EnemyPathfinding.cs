using System.Collections.Generic;
using LaserDefender.Data;
using UnityEngine;

namespace LaserDefender.Enemies
{
    public class EnemyPathfinding : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        private EnemyWaveData _enemyWaveData;
        
        private List<Transform> _enemyWaypoints;
        private int _waypointIndex;

        private void Awake()
        {
            _enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void Start()
        {
            _enemyWaveData  = _enemySpawner.GetCurrentWave();
            _enemyWaypoints = _enemyWaveData.GetWaypoints();
            
            transform.position = _enemyWaypoints[_waypointIndex].position;
        }

        private void Update()
        {
            FollowPath();
        }

        private void FollowPath()
        {
            if (_waypointIndex < _enemyWaypoints.Count)
            {
                var targetPosition = _enemyWaypoints[_waypointIndex].position;
                var delta = _enemyWaveData.GetMoveSpeed() * Time.deltaTime;
                
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
                
                if(transform.position == targetPosition) 
                    _waypointIndex++;
            }
            
            else
                Destroy(gameObject);
        }
    }
}