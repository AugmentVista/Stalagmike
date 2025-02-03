using System;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class AttackBehavior:FoeBehavior
    {
        internal Hitbox hitbox;

        internal override void Execute()
        {
            // Keep the warning here.
            base.Execute();

            hitbox.enabled = true;
        }
    }
}