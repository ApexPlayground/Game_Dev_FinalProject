using UnityEngine;

namespace Traps
{
    public class ArrowTrap : MonoBehaviour
    {
        public float attackCooldown;
        public Transform firePoint;
        public GameObject[] arrows;
        private float _cooldownTimer;

        // ReSharper disable Unity.PerformanceAnalysis
        private void Attack()
        {
            _cooldownTimer = 0;

            arrows[FindArrow()].transform.position = firePoint.position;
            arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
        }
        private int FindArrow()
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                if (!arrows[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }
        private void Update()
        {
            _cooldownTimer += Time.deltaTime;

            if (_cooldownTimer >= attackCooldown)
            {
                Attack();
            }
        }
    }
}