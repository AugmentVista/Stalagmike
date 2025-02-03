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

        internal virtual void Init()
        {
            Debug.Log("This should probably be overridden.");
        }
    }
}