using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header ("Game Over")]
        public GameObject gameOverScreen;
    

        [Header("Pause")]
        public GameObject pauseScreen;

        private void Awake()
        {
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               
                PauseGame(!pauseScreen.activeInHierarchy);
            }
        }

        #region Game Over
        //Activate game over screen
        public void GameOver()
        {
            gameOverScreen.SetActive(true);
        
        }

        //Restart level
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Main Menu
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        //Quit game/exit play mode if in Editor
        public void Quit()
        {
            Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)
#endif
        }
        #endregion

        #region Pause
        public void PauseGame(bool status)
        {
            //If status == true pause | if status == false unpause
            pauseScreen.SetActive(status);

            //When pause status is true change timescale to 0 (time stops)
            //when it's false change it back to 1 (time goes by normally)
            if (status)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
        #endregion
    
        public void Tutorial()
        {
            SceneManager.LoadScene(2);
        }
    }
}