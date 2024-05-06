using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : MonoBehaviour
    {
        [Header ("Attack Parameters")]
        public float attackCooldown;
        public float range;
        public int damage;

        [Header("Collider Parameters")]
        public float colliderDistance;
        public CapsuleCollider2D capsuleCollider;

        [Header("Player Layer")]
        public LayerMask playerLayer;
        private float _cooldownTimer = Mathf.Infinity;

        //References
        private Animator _anim;
        private Health.Health _playerHealth;
        private EnemyPatrol _enemyPatrol;
        private static readonly int MeleeAttack = Animator.StringToHash("meleeAttack");

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;

            //Attack only when player in sight?
            if (PlayerInSight())
            {
                if (_cooldownTimer >= attackCooldown)
                {
                    _cooldownTimer = 0;
                    _anim.SetTrigger(MeleeAttack);
                }
            }

            if (_enemyPatrol != null)
                _enemyPatrol.enabled = !PlayerInSight();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private bool PlayerInSight()
        {
            var bounds = capsuleCollider.bounds;
            var transform1 = transform;
            var direction = transform1.right * Mathf.Sign(transform1.localScale.x);
            var distance = range + colliderDistance;
            var size = new Vector2(bounds.size.x * colliderDistance, bounds.size.y); // Size of the capsule in the cast

            RaycastHit2D hit = Physics2D.CapsuleCast(bounds.center, size, capsuleCollider.direction, 0, direction, distance, playerLayer);

            if (hit.collider != null)
                _playerHealth = hit.transform.GetComponent<Health.Health>();

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var transform1 = transform;
            var bounds = capsuleCollider.bounds;
            Gizmos.DrawWireCube(bounds.center + transform1.right * range * transform1.localScale.x * colliderDistance,
                new Vector3(bounds.size.x * range, bounds.size.y, bounds.size.z));
        }

        private void DamagePlayer()
        {
            if (PlayerInSight())
                _playerHealth.TakeDamage(damage);
        }
    }
}