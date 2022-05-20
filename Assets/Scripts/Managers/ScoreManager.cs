using UnityEngine;

namespace LaserDefender.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private int score;
        private static ScoreManager instance;

        private void Awake()
        {
            ManageSingleton();
        }

        private void ManageSingleton()
        {
            if (instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public int GetScore() => score;

        public void ModifyScore(int value)
        {
            score += value;
            Mathf.Clamp(score, 0, int.MaxValue);
        }

        public void ResetScore() => score = 0;
    }
}
