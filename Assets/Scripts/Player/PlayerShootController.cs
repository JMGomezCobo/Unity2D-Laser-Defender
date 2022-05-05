using System.Collections;
using LaserDefender.Managers;
using UnityEngine;

namespace LaserDefender.Player
{
    public class PlayerShootController : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float projectileLifetime = 5f;
        [SerializeField] private float baseFiringRate = 0.2f;

        [Header("AI")]
        [SerializeField]
        private bool useAI;
        [SerializeField] private float firingRateVariance = 0f;
        [SerializeField] private float minimumFiringRate = 0.1f;

        [HideInInspector] public bool isFiring;

        private Coroutine firingCoroutine;
        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }

        private void Start()
        {
            if(useAI)
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
            if (isFiring && firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
            
            else if (!isFiring && firingCoroutine != null)
            {
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }
        }

        private IEnumerator FireContinuously()
        {
            while(true)
            {
                var instance = Instantiate(projectilePrefab, 
                    transform.position, 
                    Quaternion.identity);

                var rigidbody = instance.GetComponent<Rigidbody2D>();
                
                if (rigidbody != null)
                {
                    rigidbody.velocity = transform.up * projectileSpeed;
                }

                Destroy(instance, projectileLifetime);

                var timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

                _audioManager.PlayShootingClip();

                yield return new WaitForSeconds(timeToNextProjectile);
            }
        }
    }
}