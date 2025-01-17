using Assets.Scripts.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class HealthSystem:MonoBehaviour
    {
        public Action OnDeath = delegate { };
        public int health { get; protected set; } = 1;
        

        public void TakeDamage(HitInfo hit)
        {
            health -= hit.damage;

            if (health < 0) { OnDeath(); }
        }
    }
}
