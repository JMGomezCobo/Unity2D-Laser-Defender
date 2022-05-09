using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LaserDefender.Player
{
    public class ShootController : MonoBehaviour
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

        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks _shootFeedbacks;
        
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

                _shootFeedbacks.PlayFeedbacks();

                yield return new WaitForSeconds(timeToNextProjectile);
            }
        }
    }
}