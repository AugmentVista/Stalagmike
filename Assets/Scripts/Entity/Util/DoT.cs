namespace Assets.Scripts.Entity.Util
{
    /// <summary>
    /// Represents a damage over time effect.
    /// </summary>
    internal struct DoT
    {
        /// <summary>
        /// How many more times should this effect be ticked?
        /// </summary>
        public int TicksLeft { get; private set; }
        int damage;

        /// <summary>
        /// Create a damage over time instance.
        /// </summary>
        /// <param name="duration">How long this effect should last in ticks.</param>
        /// <param name="damage">How much damage this effect should deal per tick.</param>
        public DoT(int duration, int damage)
        {
            TicksLeft = duration;
            this.damage = damage;
        }

        /// <summary>
        /// Gets the damage from this Damage over Time instance, and decrements the timer.
        /// </summary>
        /// <returns>The amount of damage to be dealt.</returns>
        public int Tick()
        {
            TicksLeft--;
            return damage;
        }
    }
}
