using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombat
{
    public interface IHealable
    {
        void ReceiveHeal(int healAmount);
    }
}
