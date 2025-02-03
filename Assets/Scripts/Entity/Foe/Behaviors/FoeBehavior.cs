using System;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal abstract class FoeBehavior
    {
        internal PlayerDetector detector;

        internal virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}