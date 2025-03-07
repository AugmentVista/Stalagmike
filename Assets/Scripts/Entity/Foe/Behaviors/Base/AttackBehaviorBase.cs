using Assets.Scripts.Entity.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class AttackBehaviorBase : FoeBehavior
    {
        #region stats
        /// <summary>
        /// How long should the attack take to charge. This should in some way relate to the telegraph animation.
        /// </summary>
        [SerializeField] protected int startupTime = 20;
        /// <summary>
        /// When should our hitbox retract?
        /// </summary>
        [SerializeField] protected int retractionTime = 25;
        /// <summary>
        /// How long should the attack take before its usable again?
        /// </summary>
        [SerializeField] protected int cooldownTicks = 50;
        [SerializeField] protected HitInfo hit;
        #endregion

        internal Hitbox hitbox;
        [SerializeField] string animationStateName = "Attack";

        /// <summary>
        /// Used in conjunction with a tick based timer to track cooldowns.
        /// </summary>
        protected List<FoeBase> foesThisIsActiveFor = new();

        internal override void Execute(FoeBase parent)
        {
            if (!foesThisIsActiveFor.Contains(parent))
            {
                // If we have an animator, try to play the attack animation.
                if (TryGetComponent(out Animator animator))
                {
                    animator.Play(animationStateName);
                }

                // The time since last activation in ticks.
                int timer = 0;

                // Define a method that we'll use to hook into the foe's physicsprocess to time things.
                void AttackTickingInternal()
                {
                    try
                    {
                        // These cannot be a switch case due to requiring runtime constants.
                        if (timer == startupTime) { hitbox.enabled = true; }
                        if (timer == retractionTime) { hitbox.enabled = false; }

                        // End with a cooldown check.
                        if (timer == cooldownTicks)
                        {
                            parent.PhysicsProcess -= AttackTickingInternal;
                            foesThisIsActiveFor.Remove(parent);
                        }
                    }
                    catch (Exception e) { Debug.LogError(e); }
                }
                parent.PhysicsProcess += AttackTickingInternal;
                foesThisIsActiveFor.Add(parent);
            }
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