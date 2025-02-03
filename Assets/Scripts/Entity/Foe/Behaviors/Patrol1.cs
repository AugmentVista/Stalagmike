using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    [CreateAssetMenu(menuName ="FoeBehaviors/Patrol1")]
    internal class Patrol1:PatrolBehaviorBase
    {
        internal override void Execute(Class1 parent)
        {
            // get ref
            Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
            // calculate target horizontal direction as a float. (we really only care about its sign.)
            float hPos = rb.position.x;
            float targetHVel = isForward ? hPos - start.x : hPos - end.x;
            // calculate new velocity and apply it.
            Vector2 targetVel = new Vector2(targetHVel, 0).normalized * targetSpeed;
            rb.velocity = Vector2.Lerp(rb.velocity, targetVel, accel);
        }

        protected override void OnPlayerDetected(PlayerController player)
        {
            base.OnPlayerDetected(player);
            SetState(Class1.AIState.Chase);
        }

        protected override void OnPlayerLost()
        {
            base.OnPlayerLost();

            // Yes, this is here because we're relying on the patrol trigger to determine chase.
            SetState(Class1.AIState.Patrol);
        }
    }
}
