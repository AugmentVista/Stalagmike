using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal abstract class FoeBehavior:ScriptableObject
    {
        internal Action<FoeBase.AIState> SetState;
        internal PlayerDetector playerDetector;
        protected PlayerController player;

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