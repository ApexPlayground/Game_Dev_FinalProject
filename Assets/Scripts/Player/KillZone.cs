using UnityEngine;

namespace Player
{
    public class KillZoneScript : MonoBehaviour
    {
     
        public GameManger gameManager;

        public float damage;
        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManger>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
               
                col.GetComponent<PlayerRespawn>().RespawnCheck();
                col.GetComponent<Health.Health>().TakeDamage(damage);
                
            }
        }
    }
}