using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    [CreateAssetMenu(menuName = "FoeBehaviors/Chase1")]
    internal class Chase1:ChaseBehaviorBase
    {
        internal override void Execute(Class1 parent)
        {
            // get ref
            Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
            // calculate target horizontal direction as a float. (we really only care about its sign.)
            float hPos = rb.position.x;
            float targetHVel = hPos - parent.Target.transform.position.x;
            // calculate new velocity and apply it.
            Vector2 targetVel = new Vector2(targetHVel, rb.velocity.y).normalized * targetSpeed;
            rb.velocity = Vector2.Lerp(rb.velocity, targetVel, accel);
        }

        protected override void OnPlayerDetected(PlayerController player)
        {
            Attack(null); // THIS IS BAD, FIX SOON!
        }
    }
}
