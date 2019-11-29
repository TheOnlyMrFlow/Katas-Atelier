using System;

namespace RPGCombat
{
    public class Character
    {
        public int Health { get; set; }
        public int Level { get; set; }
        public bool IsAlive { get => Health > 0; }

        public bool IsDead { get => !IsAlive; }

        public Character()
        {
            Health = 1000;
            Level = 1;
        }

        public void DealDamage(Character receiver, int amount)
        {
            if (this == receiver)
                return;

            if (receiver.IsFiveOrMoreLevelHigherThan(this))
                amount = (int) (amount * 0.5f);
            else if (this.IsFiveOrMoreLevelHigherThan(receiver))
                amount = (int)(amount * 1.5f);

            receiver.Health = Math.Max(0, receiver.Health - amount);
        }

        public void HealSelf(int amount)
        {
            if (IsAlive)
                Health = Math.Min(1000, Health + amount);
        }

        private bool IsFiveOrMoreLevelHigherThan(Character other)
        {
            return this.Level - other.Level >= 5;
        }
    }
}
