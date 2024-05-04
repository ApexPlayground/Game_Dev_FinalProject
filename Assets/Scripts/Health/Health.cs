using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }
    private Animator _anim;
    private bool _dead;
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private static readonly int Die = Animator.StringToHash("die");

    private void Start()
    {
        CurrentHealth = startingHealth;
        _anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            _anim.SetTrigger(Hurt);
           
        }
        else
        {
            if (!_dead)
            {
                _anim.SetTrigger(Die);
                GetComponent<PlayerMovement>().enabled = false;
                _dead = true;
            }
        }
    }
    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, startingHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}