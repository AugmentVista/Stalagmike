using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class PatrolBehaviorBase:FoeBehavior
    {
        [SerializeField] protected Vector2 start;
        [SerializeField] protected Vector2 end;
        [SerializeField] protected float targetSpeed = 2;
        [SerializeField] protected float accel = 0.95f; // as a factor of target velocity

        protected bool isForward = true;
    }
}
