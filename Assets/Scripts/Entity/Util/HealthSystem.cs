using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class HealthSystem : MonoBehaviour
    {
        /// <summary>
        /// If disabled, we wont spam the logs when OnDeath is called.
        /// </summary>
        [SerializeField] bool debugMe = false;
        public int maxHp = 1;
        /// <summary>
        /// Called when reaching 0 hp.
        /// </summary>
        public Action OnDeath;
        public GameObject OnHitFeedback;
        public int health { get; protected set; } = 0;

        private void Awake()
        {
            health = maxHp;
        }

        private void Start()
        {
            // Add a lambda to improve debugging, and prevent errors when calling this without anything assigned.
            OnDeath += delegate { if (debugMe) { Debug.Log($"{name} was killed."); health = maxHp; } };
        }

        /// <summary>
        /// Makes this HealthSystem take damage. If hp falls to or below 0, OnDeath is called.
        /// </summary>
        /// <param name="hit">The data associated with the hit we're taking.</param>
        public void TakeDamage(HitInfo hit)
        {
            if (hit.damage<=0) { Debug.LogWarning("HitInfo.damage probably shouldn't be negative, but I'm guessing I was too lazy to do it right."); }
            health -= hit.damage;

            if (health > maxHp) { health = maxHp; Debug.Log("HP exceeded max, set HP to max."); }

            Debug.Log($"Took {hit.damage} damage, now at {health} hp.");
            
            if (OnHitFeedback!=null) { Instantiate(OnHitFeedback, transform).transform.parent = null; }
            if (health <= 0) { OnDeath(); }
        }

        public void Heal(int healValue, int maxHealthValue)
        {
            maxHp += maxHealthValue;
            health += healValue;
            if (health > maxHp) 
            {
                health = maxHp; Debug.Log("HP exceeded max, set HP to max."); 
            }
            Debug.Log($"PLAYER HEALTH IS {health} AND MAX HP IS {maxHp}");
        }
    }
}
