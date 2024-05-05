using UnityEngine;

namespace Traps
{
    public class SpikeHead : EnemyDamage
    {
        [Header("SpikeHead Attributes")]
        public float speed;
        public float range;
        public float checkDelay;
        public LayerMask playerLayer;
        private readonly Vector3[] _directions = new Vector3[4];
        private Vector3 _destination;
        private float _checkTimer;
        private bool _attacking;

        private void OnEnable()
        {
            Stop();
        }
        private void Update()
        {
            //Move spikehead to destination only if attacking
            if (_attacking)
                transform.Translate(_destination * (Time.deltaTime * speed));
            else
            {
                _checkTimer += Time.deltaTime;
                if (_checkTimer > checkDelay)
                    CheckForPlayer();
            }
        }
        private void CheckForPlayer()
        {
            CalculateDirections();

            //Check if spikehead sees player in all 4 directions
            for (int i = 0; i < _directions.Length; i++)
            {
                var position = transform.position;
                Debug.DrawRay(position, _directions[i], Color.red);
                RaycastHit2D hit = Physics2D.Raycast(position, _directions[i], range, playerLayer);

                if (hit.collider != null && !_attacking)
                {
                    _attacking = true;
                    _destination = _directions[i];
                    _checkTimer = 0;
                }
            }
        }
        private void CalculateDirections()
        {
            var transform1 = transform;
            var right = transform1.right;
            _directions[0] = right * range; //Right direction
            _directions[1] = -right * range; //Left direction
            var up = transform1.up;
            _directions[2] = up * range; //Up direction
            _directions[3] = -up * range; //Down direction
        }
        private void Stop()
        {
            _destination = transform.position; //Set destination as current position so it doesn't move
            _attacking = false;
        }

        private new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            Stop();
        }
    }
}