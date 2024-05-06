using Traps;
using UnityEngine;

namespace Enemies
{
    public class RangedEnemy : MonoBehaviour
    {
        [Header("Attack Parameters")]
        public float attackCooldown;
        public float range;
        public int damage;

        [Header("Ranged Attack")]
        public Transform firepoint;
        public GameObject[] fireballs;

        [Header("Collider Parameters")]
        public float colliderDistance;
        public CapsuleCollider2D capsuleCollider;

        [Header("Player Layer")]
        public LayerMask playerLayer;
        private float _cooldownTimer = Mathf.Infinity;

        //References
        private Animator _anim;
        private EnemyPatrol _enemyPatrol;
        private static readonly int Attack = Animator.StringToHash("rangedAttack");

        private void Awake()
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
                    _anim.SetTrigger(Attack);
                }
            }

            if (_enemyPatrol != null)
                _enemyPatrol.enabled = !PlayerInSight();
        }

        private void RangedAttack()
        {
            _cooldownTimer = 0;
            fireballs[FindFireball()].transform.position = firepoint.position;
            fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
        }
        private int FindFireball()
        {
            for (int i = 0; i < fireballs.Length; i++)
            {
                if (!fireballs[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }

        private bool PlayerInSight()
        {
            var bounds = capsuleCollider.bounds;
            var direction = transform.right * Mathf.Sign(transform.localScale.x);
            var size = new Vector2(bounds.size.x * colliderDistance, bounds.size.y);
            var distance = range;

            RaycastHit2D hit = Physics2D.CapsuleCast(
                bounds.center,
                size,
                capsuleCollider.direction,
                0,
                direction,
                distance,
                playerLayer
            );

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var bounds = capsuleCollider.bounds;
            var transform1 = transform;
            Gizmos.DrawWireCube(bounds.center + transform1.right * range * transform1.localScale.x * colliderDistance,
                new Vector3(bounds.size.x * range, bounds.size.y, bounds.size.z));
        }
    }
}