using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public float attackCooldown;
    public Transform firePoint;
    public GameObject[] fireballs;

    private Animator _anim;
    private PlayerMovement _playerMovement;
    private float _cooldownTimer = Mathf.Infinity;

    private static readonly int Attack1 = Animator.StringToHash("attack");

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _cooldownTimer > attackCooldown && _playerMovement.CanAttack())
            Attack();

        _cooldownTimer += Time.deltaTime;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Attack()
    {
        _anim.SetTrigger(Attack1);
        _cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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
}