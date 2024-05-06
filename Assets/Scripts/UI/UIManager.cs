using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject gameOverScreen;
        

        private void Awake()
        {
            gameOverScreen.SetActive(false);
        }

        #region Game Over Functions
        //Game over function
        public void GameOver()
        {
            gameOverScreen.SetActive(true);
           
        }

        //Restart level
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Activate game over screen
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        //Quit game/exit play mode if in Editor
        public void Quit()
        {
            Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
        }
        #endregion
    }
}