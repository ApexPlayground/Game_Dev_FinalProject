using UnityEngine;

namespace Enemies
{
    public class EnemyFireballHolder : MonoBehaviour
    {
        public Transform enemy;

        private void Update()
        {
            transform.localScale = enemy.localScale;
        }
    }
}