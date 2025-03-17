using Assets.Scripts.Entity.Foe.Behaviors;
using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe
{
    internal class FoeBase : MonoBehaviour
    {
        // Refs go here, idk what i need.
        [Header("Refs")]
        [SerializeField] protected GroundDetector groundDetector;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected HealthSystem healthSystem;
        public Action PhysicsProcess;
        protected Action<AIState> StateChanged;

        // Behaviors
        [SerializeField] PatrolBehaviorBase patrol;
        [SerializeField] PlayerDetector patrolPlayerDetector;
        [SerializeField] ChaseBehaviorBase chase;
        [SerializeField] PlayerDetector chasePlayerDetector;
        [SerializeField] AttackBehaviorBase attack;
        [SerializeField] Hitbox attackHitbox;

        public PlayerController Target { get; private set; }
        protected AIState State { get { return state; } set { StateChanged(value); state = value; } }
        AIState state;

        protected virtual void Start()
        {
            // Set refs
            patrol.playerDetector = patrolPlayerDetector;
            chase.playerDetector = chasePlayerDetector;
            attack.hitbox = attackHitbox;
            chase.Attack = attack.Execute;

            patrolPlayerDetector.PlayerDetected += OnPlayerDetected;
            patrolPlayerDetector.PlayerLost += OnPlayerLost;
            PhysicsProcess = _PhysicsProcess;
            StateChanged = _StateChanged;
            healthSystem.OnDeath += OnDeath;

            // Add setstate actions
            patrol.SetState = SetState;
            chase.SetState = SetState;
            attack.SetState = SetState;

            // Initialize everything.
            patrol.Init();
            chase.Init();
            attack.Init();

            void SetState(AIState state)
            {
                State = state;
            }
        }

        private void FixedUpdate()
        {
            PhysicsProcess();
        }

        void _PhysicsProcess()
        {
            switch (State)
            {
                case AIState.Patrol: patrol.Execute(); break;
                case AIState.Chase: chase.Execute(); break;
            }
        }

        protected virtual void _StateChanged(AIState state)
        {
            Debug.Log($"Foe {name} state set to {state}");
        }

        private void OnPlayerDetected(PlayerController controller)
        {
            Target = controller;
            Debug.Log($"Foe {name} found player {controller.name}");
        }

        protected virtual void OnPlayerLost()
        {
            Target = null;
        }

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }

        public enum AIState
        {
            Patrol,
            Chase,
        }
    }
}
