using UnityEngine;

namespace Player
{
    public class GameManger : MonoBehaviour
    {
       
        public float playerScore;
        public GameObject player;
        
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void AddScore(float score)
        {
            playerScore += score;
        }
        
    }
}