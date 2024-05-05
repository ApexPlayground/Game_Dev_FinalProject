using UnityEngine;

namespace Traps
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] protected float damage;

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<Health.Health>().TakeDamage(damage);
        }
    }
}