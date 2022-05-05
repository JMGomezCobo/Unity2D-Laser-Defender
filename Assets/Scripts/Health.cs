using LaserDefender.AI;
using LaserDefender.Managers;
using UnityEngine;

namespace LaserDefender
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private bool isPlayer;
        [SerializeField] private int health = 50;
        [SerializeField] private int score = 50;
        [SerializeField] private ParticleSystem hitEffect;

        [SerializeField] private bool applyCameraShake;
        private CameraShake cameraShake;

        private AudioManager _audioManager;
        private ScoreManager _scoreManager;
        private LevelManager levelManager;

        private void Awake()
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
            
            _audioManager = FindObjectOfType<AudioManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageDealer = other.GetComponent<DamageDealer>();

            if (damageDealer == null) return;
            
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            
            _audioManager.PlayDamageClip();
            
            ShakeCamera();
            damageDealer.Hit();
        }

        public int GetHealth()
        {
            return health;
        }

        private void TakeDamage(int damage)
        {
            health -= damage;
            
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (!isPlayer)
            {
                _scoreManager.ModifyScore(score);
            }
            
            else
            {
                levelManager.LoadGameOver();
            }
            
            Destroy(gameObject);
        }

        private void PlayHitEffect()
        {
            if (hitEffect == null) return;
            
            var instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            var main = instance.main;
                
            Destroy(instance.gameObject, main.duration + main.startLifetime.constantMax);
        }

        private void ShakeCamera()
        {
            if (cameraShake != null && applyCameraShake)
            {
                cameraShake.Play();
            }
        }
    }
}