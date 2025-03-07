using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    [CreateAssetMenu(menuName = "FoeBehaviors/Patrol1")]
    internal class Patrol1 : PatrolBehaviorBase
    {
        internal override void Execute()
        {
            // get ref
            Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
            // calculate target horizontal direction as a float. (we really only care about its sign.)
            float hPos = rb.position.x;
            float targetDirection = isForward ? start.x - hPos : end.x - hPos;
            // calculate new velocity and apply it.
            float targetHVel = (new Vector2(targetDirection, 0).normalized * targetSpeed).x;
            rb.velocity = Vector2.Lerp(rb.velocity, new(targetHVel, rb.velocity.y), accel);

            if (Vector2.Distance(rb.position, start) < targetSpeed) { isForward = false; }
            else if (Vector2.Distance(rb.position, end) < targetSpeed) { isForward = true; }
        }

        protected override void OnPlayerDetected(PlayerController player)
        {
            base.OnPlayerDetected(player);
            SetState(FoeBase.AIState.Chase);
        }

        protected override void OnPlayerLost()
        {
            base.OnPlayerLost();

            // Yes, this is here because we're relying on the patrol trigger to determine chase.
            SetState(FoeBase.AIState.Patrol);
        }
    }
}
