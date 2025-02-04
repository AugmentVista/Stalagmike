using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class AttackBehaviorBase:FoeBehavior
    {
        [SerializeField] protected HitInfo hit;
        internal Hitbox hitbox;

        internal override void Execute(FoeBase parent)
        {
            // Keep the warning here.
            base.Execute(parent);

            hitbox.enabled = true; // TODO: Currently this produces an infinite duration bug. Pls fix later.
        }

        internal override void Init()
        {
            hitbox.OnHit += OnHit;
        }

        protected virtual void OnHit(HealthSystem healthSystem)
        {
            healthSystem.TakeDamage(hit);
            hitbox.enabled = false;
        }
    }
}