using System;
using System.Collections.Generic;
using System.Text;

namespace Domaine
{
    public class StatistiqueProduit //Entity
    {
        public StatistiqueProduit(uint idProduit)
        {
            this.Produit = idProduit;
        }

        private readonly object _lockQuantite = new object();
        private readonly object _lockChiffreDaffaire = new object();


        public uint Produit { get; }

        public int QuantitéVendue { get => _quantiteVendue; }
        private int _quantiteVendue = 0;
        public decimal ChiffreDAffaire { get => _chiffreDaffaire; }
        private decimal _chiffreDaffaire = 0;

        public void AjouterQuantite(int quantite)
        {
            lock (_lockQuantite)
            {
                _quantiteVendue += quantite;
            }
        }

        public void AjouterChiffreDAffaire(decimal chiffreDaffaire)
        {
            lock (_lockChiffreDaffaire)
            {
                _chiffreDaffaire += chiffreDaffaire;
            }
        }

    }
}
