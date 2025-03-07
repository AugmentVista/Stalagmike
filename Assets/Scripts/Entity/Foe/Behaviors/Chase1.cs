using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class Chase1 : ChaseBehaviorBase
    {
        internal override void Execute()
        {
            string name = Parent.name;
            // get ref
            Rigidbody2D rb = Parent.GetComponent<Rigidbody2D>();
            // calculate target horizontal direction as a float. (we really only care about its sign.)
            float hPos = rb.position.x;
            float targetDirection = Parent.Target.transform.position.x - hPos;
            // calculate new velocity and apply it.
            float targetHVel = (new Vector2(targetDirection, 0).normalized * targetSpeed).x;
            rb.velocity = Vector2.Lerp(rb.velocity, new(targetHVel, rb.velocity.y), accel);
        }

        protected override void OnPlayerDetected(PlayerController player)
        {
            Attack();
        }
    }
}
