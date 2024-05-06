using UI;
using UnityEngine;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        private Transform _currentCheckpoint;
        private Health.Health _playerHealth;
        private static readonly int Activate = Animator.StringToHash("appear");
        private UIManager _uiManager;

        private void Start()
        {
            _playerHealth = GetComponent<Health.Health>();
             _uiManager = FindObjectOfType<UIManager>();
        }

        public void RespawnCheck()
        {
            if (_currentCheckpoint == null) 
            {
                _uiManager.GameOver();
                return;
            }

            _playerHealth.Respawn(); //Restore player health and reset animation
            transform.position = _currentCheckpoint.position; //Move player to checkpoint location

           
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Checkpoint"))
            {
                _currentCheckpoint = collision.transform;
                collision.GetComponent<Collider2D>().enabled = false;
                collision.GetComponent<Animator>().SetTrigger(Activate);
            }
        }
    }
}