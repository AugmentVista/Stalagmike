using System;

namespace Assets.Scripts.Entity.Foe
{
    internal abstract class FoeBehavior
    {
        internal Action Attack;

        internal virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}