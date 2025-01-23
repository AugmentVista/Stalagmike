using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe
{
    internal class FoeBase:MonoBehaviour
    {
        // Refs go here, idk what i need.
        [Header("Refs")]
        [SerializeField] protected GroundDetector groundDetector;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected HealthSystem healthSystem;
        Action PhysicsProcess = delegate { };

        private void OnEnable()
        {
            healthSystem.OnDeath += OnDeath;
        }
        private void OnDisable()
        {
            healthSystem.OnDeath -= OnDeath;
            Debug.Log("blerg");
        }

        private void FixedUpdate()
        {
            HandleMovement();
            PhysicsProcess();
        }

        /// <summary>
        /// This is where we'll do movement stuffs.
        /// </summary>
        protected virtual void HandleMovement()
        {
            Debug.Log("do movement things here. this isn't implemented yet.");
        }

        protected virtual void OnDeath()
        {
            Debug.Log("Death stuff here.");

            Destroy(gameObject);

            // Ideally, we want to get component refs to limbs and send them flying on death, but that is stretch goal.
        }
    }
}
