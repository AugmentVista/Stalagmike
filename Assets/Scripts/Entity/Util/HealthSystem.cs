﻿using Assets.Scripts.Entity.Util;
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
        [SerializeField]public int maxHp = 1;
        /// <summary>
        /// Called when reaching 0 hp.
        /// </summary>
        public Action OnDeath;
        public GameObject OnHitFeedback;
        public int health { get; protected set; } = 0;

        private void Start()
        {
            health = maxHp;
            //Debug.Log($"HealthSystem displays {health}");

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

            Debug.Log($"Took {hit.damage} damage, now at {health} hp.");

            if (OnHitFeedback!=null) { Instantiate(OnHitFeedback, transform).transform.parent = null; }
            if (health <= 0) { OnDeath(); }
        }
    }
}
