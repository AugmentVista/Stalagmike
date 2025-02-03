using System;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal abstract class FoeBehavior
    {
        internal PlayerDetector playerDetector;

        internal virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}