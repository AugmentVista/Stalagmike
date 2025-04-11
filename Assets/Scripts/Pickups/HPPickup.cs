using UnityEngine;

namespace Assets.Scripts.Pickups
{
    class HPPickup : MonoBehaviour
    {
        [SerializeField] int healValue;

        // This must be a factor of 2
        [SerializeField] int maxHealthUp = 2;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HealthSystem hs) && collision.TryGetComponent(out PlayerController ignored))
            {
                // Deal negative damage to make it quicker.
                hs.Heal(healValue, maxHealthUp);
                Debug.LogWarning($"Max health increased to {hs.maxHp}");
                Destroy(gameObject);
            }
        }
    }
}
