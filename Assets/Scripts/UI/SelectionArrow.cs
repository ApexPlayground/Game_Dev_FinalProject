using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectionArrow : MonoBehaviour
    {
        public RectTransform[] buttons;
        private RectTransform _arrow;
        private int _currentPosition;

        private void Awake()
        {
            _arrow = GetComponent<RectTransform>();
        }
        private void OnEnable()
        {
            _currentPosition = 0;
            ChangePosition(0);
        }
        private void Update()
        {
            //Change the position of the selection arrow
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                ChangePosition(-1);
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                ChangePosition(1);

            //Interact with current option
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
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
            //Assign the Y position of the current option to the arrow (basically moving it up and down)
            _arrow.position = new Vector3(_arrow.position.x, buttons[_currentPosition].position.y);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private void Interact()
        {
            //Access the button component on each option and call its function
            buttons[_currentPosition].GetComponent<Button>().onClick.Invoke();
        }
    }
}