using UnityEngine;

namespace Assets.Scripts.Pickups
{
    class HPPickup : MonoBehaviour
    {
        [SerializeField] int value;

        // This must be a factor of 2
        [SerializeField] int maxHealthIncrease = 2;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HealthSystem hs) && collision.TryGetComponent(out PlayerController ignored))
            {
                // Deal negative damage to make it quicker.
                hs.TakeDamage(value * -1);
                hs.maxHp += maxHealthIncrease;
                Destroy(gameObject);
            }
        }
    }
}
