using System;

namespace Assets.Scripts.Entity.Foe
{
    internal abstract class FoeBehavior : AttackBehavior
    {
        internal virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}