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
        /// <summary>
        /// Used in conjunction with a tick based timer to track cooldowns.
        /// </summary>
        protected List<FoeBase> foesThisIsActiveFor = new();

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