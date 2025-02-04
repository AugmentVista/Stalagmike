using Assets.Scripts.Entity.Foe.Behaviors;
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
        protected Action PhysicsProcess;
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

        void Start()
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
                case AIState.Patrol: patrol.Execute(this); break;
                case AIState.Chase: chase.Execute(this); break;
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

        private void OnPlayerLost()
        {
            Target = null;
        }

        public enum AIState
        {
            Patrol,
            Chase,
        }
    }
}
