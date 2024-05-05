using UnityEngine;

namespace Traps
{
    public class EnemySideways : MonoBehaviour
    {
        public float movementDistance;
        public float speed;
        public float damage;
        private bool _movingLeft;
        private float _leftEdge;
        private float _rightEdge;

        private void Awake()
        {
            var position = transform.position;
            _leftEdge = position.x - movementDistance;
            _rightEdge = position.x + movementDistance;
        }

        private void Update()
        {
            if (_movingLeft)
            {
                if (transform.position.x > _leftEdge)
                {
                    var transform1 = transform;
                    var position = transform1.position;
                    position = new Vector3(position.x - speed * Time.deltaTime, position.y, position.z);
                    transform1.position = position;
                }
                else
                    _movingLeft = false;
            }
            else
            {
                if (transform.position.x < _rightEdge)
                {
                    var transform1 = transform;
                    var position = transform1.position;
                    position = new Vector3(position.x + speed * Time.deltaTime, position.y, position.z);
                    transform1.position = position;
                }
                else
                    _movingLeft = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health.Health>().TakeDamage(damage);
            }
        }
    }
}