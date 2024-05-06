using UnityEngine;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        public float playerScore;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
        public void AddScore(float score)
        {
            playerScore += score;
        }
    }
}