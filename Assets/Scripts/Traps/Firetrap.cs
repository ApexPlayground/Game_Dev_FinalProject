using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Traps
{
    public class Firetrap : MonoBehaviour
    {
        [SerializeField] private float damage;

        [Header("Firetrap Timers")]
        public float activationDelay;
        public float activeTime;
        private Animator _anim;
        private SpriteRenderer _spriteRend;

        private bool _triggered; 
        private bool _active;
        private static readonly int Activated = Animator.StringToHash("activated");

        private Health.Health _playerHealth;

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_playerHealth != null && _active)
            {
                _playerHealth.TakeDamage(damage);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerHealth = collision.GetComponent<Health.Health>();
                if (!_triggered)
                    StartCoroutine(ActivateFiretrap());

                if (_active)
                    collision.GetComponent<Health.Health>().TakeDamage(damage);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerHealth = null;
            }
        }

        private IEnumerator ActivateFiretrap()
        {
            _triggered = true;

            // Turn the sprite red to notify the player and trigger the trap
            _spriteRend.color = Color.red;

            // Wait for the activation delay
            yield return new WaitForSeconds(activationDelay);

            // Activate the trap, turn on the animation
            _active = true;
            _anim.SetBool(Activated, true);

            // Turn the sprite back to its initial color
            _spriteRend.color = Color.white;

            // Trap remains active for 'activeTime', then deactivate
            yield return new WaitForSeconds(activeTime);

            // Deactivate trap and reset all variables and animator
            _active = false;
            _triggered = false;
            _anim.SetBool(Activated, false);
        }

    }
}