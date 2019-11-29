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
            receiver.Health = Math.Max(0, receiver.Health - amount);
        }

        public void Heal(Character receiver, int amount)
        {
            if (receiver.IsAlive)
                receiver.Health = Math.Min(1000, receiver.Health + amount);
        }
    }
}
