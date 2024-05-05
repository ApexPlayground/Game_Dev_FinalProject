using System;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float startingHealth;
    public float CurrentHealth { get; private set; }
    private Animator _anim;
    private bool _dead;
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private static readonly int Die = Animator.StringToHash("die");
    [Header("iFrames")]
    public float iFramesDuration;
    public int numberOfFlashes;
    private SpriteRenderer _spriteRend;

    private void Start()
    {
        CurrentHealth = startingHealth;
        _anim = GetComponent<Animator>();
        _spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            _anim.SetTrigger(Hurt);
            StartCoroutine(Invulnerability());
           
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
    
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            Color color;
            color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            color = Color.white;
            _spriteRend.color = color;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    
}