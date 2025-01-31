using System;

namespace Assets.Scripts.Entity.Foe
{
    internal class FoeBehavior
    {
        internal PlayerDetector detector;
        internal Action Attack;

        internal virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}