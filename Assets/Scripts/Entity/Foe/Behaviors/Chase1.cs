using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    [CreateAssetMenu(menuName = "FoeBehaviors/Chase1")]
    internal class Chase1 : ChaseBehaviorBase
    {
        internal override void Execute()
        {
            string name = parent.name;
            // get ref
            Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
            // calculate target horizontal direction as a float. (we really only care about its sign.)
            float hPos = rb.position.x;
            float targetDirection = parent.Target.transform.position.x - hPos;
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
