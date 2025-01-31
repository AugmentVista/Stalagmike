using System;

namespace Assets.Scripts.Entity.Foe
{
    internal class FoeBehavior
    {
        internal Action Attack;

        internal virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}