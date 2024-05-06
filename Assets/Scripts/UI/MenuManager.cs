using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
       public RectTransform arrow;
       public RectTransform[] buttons;
       
        private int _currentPosition;

        private void Awake()
        {
            ChangePosition(0);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                ChangePosition(-1);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                ChangePosition(1);

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
                Interact();
        }

        private void ChangePosition(int change)
        {
            _currentPosition += change;

            if (change != 0)
              

                if (_currentPosition < 0)
                    _currentPosition = buttons.Length - 1;
                else if (_currentPosition > buttons.Length - 1)
                    _currentPosition = 0;

            AssignPosition();
        }
        private void AssignPosition()
        {
            arrow.position = new Vector3(arrow.position.x, buttons[_currentPosition].position.y);
        }
        private void Interact()
        {
           
            if (_currentPosition == 0)
            {
                //Start game
                SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
            }
            else if (_currentPosition == 1)
            {
                //Open Settings
            }
            else if (_currentPosition == 2)
            {
                //Open Credits
            }
            else if (_currentPosition == 3)
                Application.Quit();
        }
    }
}