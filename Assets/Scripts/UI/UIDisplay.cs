using LaserDefender.AI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LaserDefender.UI
{
    public class UIDisplay : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField]
        private Slider healthSlider;
        [SerializeField] private Health playerHealth;

        [Header("Score")]
        [SerializeField]
        private TextMeshProUGUI scoreText;

        private ScoreManager _scoreManager;

        private void Awake()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
        }

        private void Start()
        {
            healthSlider.maxValue = playerHealth.GetHealth();
        }

        private void Update()
        {
            healthSlider.value = playerHealth.GetHealth();
            scoreText.text = _scoreManager.GetScore().ToString("0000");
        }
    }
}
