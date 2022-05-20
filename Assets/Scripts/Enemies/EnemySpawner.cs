using System.Collections;
using System.Collections.Generic;
using LaserDefender.Data;
using UnityEngine;

namespace LaserDefender.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyWaveData> waveConfigs;
        [SerializeField] private float _timeBetweenWaves;
        [SerializeField] private bool _isLooping;
        
        private EnemyWaveData _currentEnemyWave;

        private void Start()
        {
            StartCoroutine(SpawnEnemyWaves());
        }

        public EnemyWaveData GetCurrentWave()
        {
            return _currentEnemyWave;
        }

        private IEnumerator SpawnEnemyWaves()
        {
            do
            {
                foreach (var wave in waveConfigs)
                {
                    _currentEnemyWave = wave;
                    
                    for (var i = 0; i < _currentEnemyWave.GetEnemyCount(); i++)
                    {
                        Instantiate(_currentEnemyWave.GetEnemyPrefab(i),
                            _currentEnemyWave.GetStartingWaypoint().position,
                            Quaternion.Euler(0,0,180),
                            transform);
                        
                        yield return new WaitForSeconds(_currentEnemyWave.GetRandomSpawnTime());
                    }
                    
                    yield return new WaitForSeconds(_timeBetweenWaves);
                }
            }
            
            while(_isLooping);
        }
    }
}