using UnityEngine;

namespace Traps
{
    public class EnemyProjectile : EnemyDamage
    {
        public float speed;
        public float resetTime;
        private float _lifetime;

        public void ActivateProjectile()
        {
            _lifetime = 0;
            gameObject.SetActive(true);
        }
        private void Update()
        {
            float movementSpeed = speed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);

            _lifetime += Time.deltaTime;
            if (_lifetime > resetTime)
                gameObject.SetActive(false);
        }

        private new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision); 
            gameObject.SetActive(false); 
        }
    }
}