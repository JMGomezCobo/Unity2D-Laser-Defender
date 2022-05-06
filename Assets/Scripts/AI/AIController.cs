using System.Collections.Generic;
using LaserDefender.AI;
using UnityEngine;

namespace LaserDefender
{
    public class AIController : MonoBehaviour
    {
        private AIManager _aiManager;
        private AIWaveData _aiWaveConfig;
        private List<Transform> waypoints;
        private int waypointIndex;
        
        private void Awake()
        {
            _aiManager = FindObjectOfType<AIManager>();
        }

        private void Start()
        {
            _aiWaveConfig = _aiManager.GetCurrentWave();
            waypoints = _aiWaveConfig.GetWaypoints();
            transform.position = waypoints[waypointIndex].position;
        }

        private void Update()
        {
            FollowPath();
        }

        private void FollowPath()
        {
            if (waypointIndex < waypoints.Count)
            {
                var targetPosition = waypoints[waypointIndex].position;
                var delta = _aiWaveConfig.GetMoveSpeed() * Time.deltaTime;
                
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
                
                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            
            else
            {
                Destroy(gameObject);
            }
        }
    }
}