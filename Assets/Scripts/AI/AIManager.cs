using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserDefender.AI
{
    public class AIManager : MonoBehaviour
    {
        [Header("Data Bindings")]
        [SerializeField] private List<AIWaveData> _waveConfigs;
        [Space]
        [SerializeField] private float _timeBetweenWaves;
        [SerializeField] private bool _isLooping;
        
        private AIWaveData _currentAIWave;

        private void Start()
        {
            StartCoroutine(SpawnEnemyWaves());
        }

        public AIWaveData GetCurrentWave() => _currentAIWave;

        private IEnumerator SpawnEnemyWaves()
        {
            do
            {
                foreach (var wave in _waveConfigs)
                {
                    _currentAIWave = wave;
                    
                    for (var i = 0; i < _currentAIWave.GetEnemyCount(); i++)
                    {
                        Instantiate(_currentAIWave.GetEnemyPrefab(i), _currentAIWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform);
                        
                        yield return new WaitForSeconds(_currentAIWave.GetRandomSpawnTime());
                    }
                    
                    yield return new WaitForSeconds(_timeBetweenWaves);
                }
            }
            
            while(_isLooping);
        }
    }
}