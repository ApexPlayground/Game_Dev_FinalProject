using TMPro;
using UnityEngine;

namespace Player
{
    public class GameUIScript : MonoBehaviour
    {
        public GameManger gameManger;
        public TMP_Text scoreText;
        
    
    
        // Start is called before the first frame update
        void Start()
        {
            gameManger = GameObject.Find("GameManager").GetComponent<GameManger>();

        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = "Score: " + gameManger.playerScore;
        

        }
    }
}