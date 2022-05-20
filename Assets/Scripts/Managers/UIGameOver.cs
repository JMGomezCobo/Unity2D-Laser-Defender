using TMPro;
using UnityEngine;

namespace LaserDefender.Managers
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private ScoreManager _scoreManager;

        private void Awake()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
        }

        private void Start()
        {
            scoreText.text = "You Scored:\n" + _scoreManager.GetScore();
        }
    }
}
