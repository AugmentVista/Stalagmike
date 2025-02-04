using Assets.Scripts.Entity.Foe.Behaviors;
using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe
{
    internal class Class1 : MonoBehaviour
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
        protected AIState State { get { return state; } set { StateChanged(value); state = value; } }
        AIState state;

        void Start()
        {
            // Set refs
            patrol.playerDetector = patrolPlayerDetector;
            chase.playerDetector = chasePlayerDetector;
            attack.hitbox = attackHitbox;
            chase.Attack = attack.Execute;

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

        public enum AIState
        {
            Patrol,
            Chase,
        }
    }
}
