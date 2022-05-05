using System.Collections;
using LaserDefender.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaserDefender.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private float sceneLoadDelay = 2f;
        private ScoreManager _scoreManager;

        private void Awake()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
        }

        public void LoadGame()
        {
            _scoreManager.ResetScore();
            SceneManager.LoadScene("Game");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void LoadGameOver()
        {
            StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private IEnumerator WaitAndLoad(string sceneName, float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
        }
    }
}