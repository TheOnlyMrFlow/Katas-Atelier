using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombat
{
    public abstract class Props : Entity, ILiving, IDamagable
    {
        public abstract int MaxHealth { get; }

        public int Health { get; set; }

        public bool IsAlive => Health > 0;

        public bool IsDead => !IsAlive;

        public Props()
        {
            Health = MaxHealth;
        }

        public void SufferDamage(int damageAmount)
        {
            Health = Math.Max(0, Health - damageAmount);
        }

        
    }
}
