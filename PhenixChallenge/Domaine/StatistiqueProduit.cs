using System;
using System.Collections.Generic;
using System.Text;

namespace Domaine
{
    class StatistiqueProduit //Entity
    {
        public StatistiqueProduit(uint idProduit)
        {
            this.IdProduit = idProduit;
        }

        private readonly object _lockQuantite = new object();
        private readonly object _lockChiffreDaffaire = new object();


        public uint IdProduit { get; }

        public int QuantiteVendue { get => _quantiteVendue; }
        private int _quantiteVendue = 0;
        public double ChiffreDaffaire { get => _chiffreDaffaire; }
        private double _chiffreDaffaire = 0;

        public void AjouterQuantite(int quantite)
        {
            lock (_lockQuantite)
            {
                _quantiteVendue += quantite;
            }
        }

        public void AjouterChiffreDAffaire(double chiffreDaffaire)
        {
            lock (_lockChiffreDaffaire)
            {
                _chiffreDaffaire += chiffreDaffaire;
            }
        }

    }
}
