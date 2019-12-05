using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombat
{
    public interface ILiving
    {
        int MaxHealth { get; }
        int Health { get; }
        bool IsAlive { get; }
        bool IsDead { get; }
    }
}
