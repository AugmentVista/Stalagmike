using Assets.Scripts.Struct;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class HealthSystem : MonoBehaviour
    {
        /// <summary>
        /// If disabled, we wont spam the logs when OnDeath is called.
        /// </summary>
        [SerializeField] bool debugMe = true;
        [SerializeField] int maxHp = 1;
        /// <summary>
        /// Called when reaching 0 hp.
        /// </summary>
        public Action OnDeath;
        public int health { get; protected set; } = 0;

        private void Start()
        {
            health = maxHp;

            // Add a lambda to improve debugging, and prevent errors when calling this without anything assigned.
            OnDeath += delegate { if (debugMe) { Debug.Log($"{name} was killed."); health = maxHp; } };
        }

        /// <summary>
        /// Makes this HealthSystem take damage. If hp falls to or below 0, OnDeath is called.
        /// </summary>
        /// <param name="hit">The data associated with the hit we're taking.</param>
        public void TakeDamage(HitInfo hit)
        {
            health -= hit.damage;

            if (health <= 0) { OnDeath(); }
        }
    }
}
