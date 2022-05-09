using LaserDefender.AI;
using LaserDefender.Managers;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LaserDefender
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private bool isPlayer;
        [SerializeField] private int health = 50;
        [SerializeField] private int score = 50;

        private ScoreManager _scoreManager;
        private LevelManager levelManager;
        
        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks _damageFeedbacks;
        [SerializeField] private MMFeedbacks _deathFeedbacks;

        private void Awake()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageDealer = other.GetComponent<DamageDealer>();

            if (damageDealer == null) return;
            
            TakeDamage(damageDealer.GetDamage());
            _damageFeedbacks.PlayFeedbacks();

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
            
            _deathFeedbacks.PlayFeedbacks();
            Destroy(gameObject);
        }
    }
}