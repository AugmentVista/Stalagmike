using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Struct
{
    /// <summary>
    /// Represents a damaging hit dealt from one object to another.
    /// </summary>
    internal struct HitInfo
    {
        public int damage;

        public HitInfo(int damage)
        {
            this.damage = damage;
        }

        // While we only track damage, this will make things easier, however we're using a struct for extendability.
        public static implicit operator HitInfo(int damage) {  return new HitInfo(damage); }
        public static implicit operator int(HitInfo hitInfo) { return hitInfo.damage; }
    }
}
