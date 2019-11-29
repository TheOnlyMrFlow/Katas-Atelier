using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RPGCombat
{
    public class Faction
    {
        public string Nom { get; set; }

        public Faction(string nom)
        {
            Nom = nom;
        }

    }
}
