using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pickups
{
    class HPPickup : MonoBehaviour
    {
        [SerializeField] int value;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HealthSystem hs) && collision.TryGetComponent(out PlayerController ignored))
            {
                // Deal negative damage to make it quicker.
                hs.TakeDamage(value * -1);
                Destroy(gameObject);
            }
        }
    }
}
