using UnityEngine;

namespace Health
{
    public class HealthCollectible : MonoBehaviour
    {
        public float healthValue;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<global::Health.Health>().AddHealth(healthValue);
                gameObject.SetActive(false);
            }
        }
    }
}