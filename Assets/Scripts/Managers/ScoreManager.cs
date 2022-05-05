using UnityEngine;

namespace LaserDefender.AI
{
    public class ScoreManager : MonoBehaviour
    {
        private int _score;

        private static ScoreManager _instance;

        private void Awake()
        {
            ManageSingleton();
        }

        private void ManageSingleton()
        {
            if (_instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public int GetScore() => _score;
        public void ResetScore() => _score = 0;
        
        public void ModifyScore(int value)
        {
            _score += value;
            Mathf.Clamp(_score, 0, int.MaxValue);
        }
    }
}