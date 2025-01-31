using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe
{
    internal class Class1:MonoBehaviour
    {
        // Refs go here, idk what i need.
        [Header("Refs")]
        [SerializeField] protected GroundDetector groundDetector;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected HealthSystem healthSystem;
        protected Action PhysicsProcess = delegate { };

        [SerializeField] foebehavior patrol, chase;
        PlayerDetector playerDetector;
        int stateInt; // replaces an enum

        void Ready()
        {
            playerDetector = GetComponent<PlayerDetector>();
            chase.detector = playerDetector;
        }

        void _PhysicsProcess()
        {
            switch (stateInt)
            {
                case 0: patrol.Execute(); break;
                case 1: chase.Execute(); break;
            }
        }

        enum AIState
        {
            Patrol,
            Chase,
        }
    }
}
