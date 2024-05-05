using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class Healthbar : MonoBehaviour
    {
        public global::Health.Health playerHealth;
        public Image totalHealthBar;
        public Image currentHealthBar;

        private void Start()
        {
            totalHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
        }
        private void Update()
        {
            currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
        }
    }
}