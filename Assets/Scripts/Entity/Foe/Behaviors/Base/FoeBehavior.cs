using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal abstract class FoeBehavior : MonoBehaviour
    {
        internal Action<FoeBase.AIState> SetState;
        internal PlayerDetector playerDetector;
        protected PlayerController player;
        [SerializeField] FoeBase _parent;
        protected FoeBase parent { get => _parent; set { _parent = value; } }

        /// <summary>
        /// Run the behavior's main action.
        /// </summary>
        /// <param name="parent">What thing is executing this code? This probably can be replaced.</param>
        internal virtual void Execute()
        {
            //Debug.LogWarning(new NotImplementedException("FoeBehavior.Execute() should be overridden."));
        }

        /// <summary>
        /// Use this for initialization, call after start/ready whatever its called.
        /// </summary>
        internal virtual void Init()
        {
            //Debug.Log("This should probably be overridden.");
            if (parent == null) { Debug.LogError("Parent was null."); }

            playerDetector.PlayerDetected += OnPlayerDetected;
            playerDetector.PlayerLost += OnPlayerLost;
        }

        protected virtual void OnPlayerLost()
        {
            player = null;
        }

        protected virtual void OnPlayerDetected(PlayerController player)
        {
            this.player = player;
        }
    }
}