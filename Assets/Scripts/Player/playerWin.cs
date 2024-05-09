using UnityEngine;
using UI;  

namespace Player
{
    public class PlayerWin : MonoBehaviour
    {
        public UIManager uiManager;
        public GameObject player;  

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Win condition met.");
                if (uiManager != null)
                {
                    uiManager.winScreen.SetActive(true);  
                    var playerMovement = player.GetComponent<PlayerMovement>();
                    if (playerMovement != null)
                    {
                        playerMovement.enabled = false;  // Disable the movement script
                    }
                }
                else
                {
                    Debug.LogError("UIManager reference not set on PlayerWin script.");
                }
            }
        }
    }
}