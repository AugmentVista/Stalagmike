using Assets.Scripts.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class HealthSystem:MonoBehaviour
    {
        [SerializeField] bool debugMe = true;
        [SerializeField] int maxHp = 1;
        public Action OnDeath;
        public int health { get; protected set; } = 0;

        private void Start()
        {
            health = maxHp;

            // Add a lambda to improve debugging, and prevent errors when calling this without anything assigned.
            OnDeath += delegate { if (debugMe) { Debug.Log($"{name} was killed."); health = maxHp; } };
        }

        public void TakeDamage(HitInfo hit)
        {
            health -= hit.damage;

            if (health <= 0) { OnDeath(); }
        }
    }
}
