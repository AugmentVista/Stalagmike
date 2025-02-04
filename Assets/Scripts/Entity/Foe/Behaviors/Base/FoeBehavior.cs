using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal abstract class FoeBehavior:MonoBehaviour
    {
        internal Action<FoeBase.AIState> SetState;
        internal PlayerDetector playerDetector;
        protected PlayerController player;

        /// <summary>
        /// Run the behavior's main action.
        /// </summary>
        /// <param name="parent">What thing is executing this code? This probably can be replaced.</param>
        internal virtual void Execute(FoeBase parent)
        {
            Debug.LogWarning(new NotImplementedException("FoeBehavior.Execute() should be overridden."));
        }

        /// <summary>
        /// Use this for initialization, call after start/ready whatever its called.
        /// </summary>
        internal virtual void Init()
        {
            Debug.Log("This should probably be overridden.");

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