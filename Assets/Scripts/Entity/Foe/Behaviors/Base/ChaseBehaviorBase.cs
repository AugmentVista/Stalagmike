using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class ChaseBehaviorBase:FoeBehavior
    {
        internal Action<Class1> Attack;

        [SerializeField] protected float targetSpeed;
        [SerializeField] protected float accel; // as a factor of target velocity
    }
}
