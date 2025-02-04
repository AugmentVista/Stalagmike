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
        internal Action<FoeBase> Attack;

        [SerializeField] protected float targetSpeed = 4;
        [SerializeField] protected float accel = 0.85f; // as a factor of target velocity
    }
}
