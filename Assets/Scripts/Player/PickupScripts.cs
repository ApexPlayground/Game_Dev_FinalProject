using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PickupScript : MonoBehaviour
    {
        public float scoreValue;
        public GameManger gameManager;
        [FormerlySerializedAs("PickupEffect")] public GameObject pickupEffect;
    
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
                gameManager.AddScore(scoreValue);
                var transform1 = transform;
                Instantiate(pickupEffect, transform1.position, transform1.rotation);
                Destroy(gameObject);
            }
        }
    }
}