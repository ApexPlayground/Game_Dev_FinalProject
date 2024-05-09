using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Game Over")]
        public GameObject gameOverScreen;

        [Header("Pause")]
        public GameObject pauseScreen;
        
        [Header("Win")]
        public GameObject winScreen;

        private void Awake()
        {
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
            winScreen.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame(!pauseScreen.activeInHierarchy);
            }
        }

        #region Game Over
        public void GameOver()
        {
            gameOverScreen.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        #endregion

        #region Pause
        public void PauseGame(bool status)
        {
            pauseScreen.SetActive(status);
            Time.timeScale = status ? 0 : 1;
        }
        #endregion

        public void Tutorial()
        {
            SceneManager.LoadScene(2);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Collision Detected with: " + collision.gameObject.name); // Log the name of the collided object

            if (collision.CompareTag("Win"))
            {
                Debug.Log("Win condition met.");
                winScreen.SetActive(true);
            }
        }

    }
}