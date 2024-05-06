using System.Collections;
using Enemies;
using UnityEngine;

namespace Health
{
    public class Health : MonoBehaviour
    {
        [Header ("Health")]
        [SerializeField] private float startingHealth;
        public float CurrentHealth { get; private set; }
        private Animator _anim;
        private bool _dead;

        [Header("iFrames")]
        public float iFramesDuration;
        public int numberOfFlashes;
        private SpriteRenderer _spriteRend;

        [Header("Components")]
        [SerializeField] private Behaviour[] components;
        private bool _invulnerable;
        private static readonly int Hurt = Animator.StringToHash("hurt");
        private static readonly int Die = Animator.StringToHash("die");
        private static readonly int Grounded = Animator.StringToHash("grounded");

        private void Start()
        {
            CurrentHealth = startingHealth;
            _anim = GetComponent<Animator>();
            _spriteRend = GetComponent<SpriteRenderer>();
        }
        public void TakeDamage(float damage)
        {
            if (_invulnerable) return;
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
                   

                    //Deactivate all attached component classes
                    foreach (Behaviour component in components)
                        component.enabled = false;
                    _anim.SetBool(Grounded, true);
                    _anim.SetTrigger(Die);

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
        
        //Respawn
        public void Respawn()
        {
            _dead = false;
            AddHealth(startingHealth);
            _anim.ResetTrigger(Die);
            _anim.Play("idle");
            StartCoroutine(Invulnerability());

            //Activate all attached component classes
            foreach (Behaviour component in components)
                component.enabled = true;
        }
    }
}