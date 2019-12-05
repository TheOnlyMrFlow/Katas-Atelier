using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombat
{
    public abstract class Entity
    {
        public int Health { get; set; }
        public abstract int MAX_HEALTH { get; }

        public Point2D Position { get; set;  }

        public virtual bool IsAlive { get => Health > 0; }
        public virtual bool IsDead { get => !IsAlive; }

        public Entity()
        {
            Health = MAX_HEALTH;
            Position = new Point2D(0, 0);
        }

        public virtual void TakeDamage(int damageAmount)
        {
            Health = Math.Max(0, Health - damageAmount);
        }

    }
}
