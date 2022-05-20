using System.Collections;
using LaserDefender.Managers;
using UnityEngine;

namespace LaserDefender
{
    public class Shooter : MonoBehaviour
    {
        [Header("Prefab Bindings")]
        [SerializeField] private GameObject projectilePrefab;
    
        [Header("Settings")]
        [SerializeField] private float _projectileSpeed = 10f;
        [SerializeField] private float _projectileLifetime = 5f;
        [SerializeField] private float _baseFiringRate = 0.2f;

        [Header("AI")]
        [SerializeField] private bool useAI;
        [SerializeField] private float firingRateVariance;
        [SerializeField] private float minimumFiringRate = 0.1f;

        [HideInInspector] public bool isFiring;

        [SerializeField] private Transform[] bulletSpawnPoints;

        private Coroutine firingCoroutine;
        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }

        private void Start()
        {
            if (useAI)
            {
                isFiring = true;
            }
        }

        private void Update()
        {
            Fire();
        }

        private void Fire()
        {
            switch (isFiring)
            {
                case true when firingCoroutine == null:
                    firingCoroutine = StartCoroutine(FireContinuously());
                    break;
            
                case false when firingCoroutine != null:
                    StopCoroutine(firingCoroutine);
                    firingCoroutine = null;
                    break;
            }
        }

        private IEnumerator FireContinuously()
        {
            while(true)
            {
                foreach (var bulletSpawnPoint in bulletSpawnPoints)
                {
                    var instance = Instantiate(projectilePrefab, bulletSpawnPoint.position, Quaternion.identity);

                    var projectileRigidbody = instance.GetComponent<Rigidbody2D>();
            
                    if (projectileRigidbody != null)
                    {
                        projectileRigidbody.velocity = transform.up * _projectileSpeed;
                    }

                    Destroy(instance, _projectileLifetime);
                }
                
                var timeToNextProjectile = Random.Range(_baseFiringRate - firingRateVariance, _baseFiringRate + firingRateVariance);
                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

                _audioManager.PlayShootingClip();
                
                yield return new WaitForSeconds(timeToNextProjectile);
            }
        }
    }
}
