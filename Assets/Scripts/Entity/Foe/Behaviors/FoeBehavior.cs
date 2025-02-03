using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal abstract class FoeBehavior
    {
        internal PlayerDetector playerDetector;

        internal virtual void Execute()
        {
            Debug.LogWarning(new NotImplementedException("FoeBehavior.Execute() should be overridden."));
        }

        /// <summary>
        /// Use this for initialization, call after start/ready whatever its called.
        /// </summary>
        internal virtual void Init()
        {
            Debug.Log("This should probably be overridden.");
        }
    }
}