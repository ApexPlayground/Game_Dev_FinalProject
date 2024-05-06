using System.Collections;
using Enemies;
using UnityEngine;

namespace Health
{
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

                    if (GetComponent<PlayerMovement>() != null)
                    {
                        GetComponent<PlayerMovement>().enabled = false;
                    }

                    //Enemy
                    if (GetComponentInParent<EnemyPatrol>() != null)
                    {
                        GetComponentInParent<EnemyPatrol>().enabled = false;
                    }
                    
                    if(GetComponent<MeleeEnemy>() != null)
                    {
                        GetComponent<MeleeEnemy>().enabled = false;
                    }
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
            // Ignore collisions between player and traps
            Physics2D.IgnoreLayerCollision(8, 9, true);

            for (int i = 0; i < numberOfFlashes; i++)
            {
                // Set the sprite color to red to indicate damage
                _spriteRend.color = new Color(1, 0, 0, 0.5f);  // Red, semi-transparent
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

                // Reset the sprite color to normal
                _spriteRend.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }

            // Re-enable collisions between player and traps
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }

    
    }
}