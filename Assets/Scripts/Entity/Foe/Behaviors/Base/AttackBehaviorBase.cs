using Assets.Scripts.Entity.Util;
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

        // The time since last activation in ticks.
        protected int timer = -1;

        internal override void Execute()
        {
            // if cooldown is over, or timer is invalid (-1) proceed.
            if (timer >= cooldownTicks||timer == -1)
            {
                // Reset the timer.
                timer = 0;

                // If we have an animator, try to play the attack animation.
                if (TryGetComponent(out Animator animator))
                {
                    animator.Play(animationStateName);
                }

                // Define a method that we'll use to hook into the foe's physicsprocess to time things.
                void AttackTickingInternal()
                {
                    //try
                    //{
                    AttackTick();

                    // End with a cooldown check and timer increment.
                    if (timer == cooldownTicks)
                    {
                        parent.PhysicsProcess -= AttackTickingInternal;
                    }
                    timer++;
                    //}
                    //catch (Exception e) { Debug.LogError(e); }

                }
                parent.PhysicsProcess += AttackTickingInternal;
            }
        }

        protected virtual void AttackTick()
        {
            // These cannot be a switch case due to requiring runtime constants.
            if (timer == startupTime) { hitbox.enabled = true; Debug.Log($"Enabled hitbox for {name}'s attack."); }
            if (timer == retractionTime) { hitbox.enabled = false; Debug.Log($"Disabled hitbox for {name}'s attack."); }
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