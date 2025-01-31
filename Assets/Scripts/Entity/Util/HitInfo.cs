using System;

namespace Assets.Scripts.Entity.Util
{
    /// <summary>
    /// Represents a damaging hit dealt from one object to another, and any additional info that may need to accompany it.
    /// </summary>
    [Serializable]
    internal struct HitInfo
    {
        public int damage;

        public HitInfo(int damage)
        {
            this.damage = damage;
        }

        // While we only track damage, this will make things easier, however we're using a struct for extendability.
        public static implicit operator HitInfo(int damage) { return new HitInfo(damage); }
        public static implicit operator int(HitInfo hitInfo) { return hitInfo.damage; }
    }
}
