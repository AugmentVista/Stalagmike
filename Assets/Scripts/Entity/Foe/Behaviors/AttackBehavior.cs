using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class AttackBehavior:FoeBehavior
    {
        [SerializeField] protected HitInfo hit;
        internal Hitbox hitbox;

        internal override void Execute()
        {
            // Keep the warning here.
            base.Execute();

            hitbox.enabled = true;
        }

        internal override void Init()
        {
            hitbox.OnHit += OnHit;
        }

        protected virtual void OnHit(HealthSystem healthSystem)
        {
            healthSystem.TakeDamage(hit);
        }
    }
}