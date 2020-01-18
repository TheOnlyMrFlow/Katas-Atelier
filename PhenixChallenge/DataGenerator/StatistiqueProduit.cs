using System;
using System.Collections.Generic;
using System.Text;

namespace DataGenerator
{
    class StatistiqueProduit //Entity
    {
        public StatistiqueProduit(uint idProduit)
        {
            this.IdProduit = idProduit;
        }

        private readonly object _lockChiffreDaffaire = new object();


        public uint IdProduit { get; }

        public int QuantiteVendue { get => _quantiteVendue; }
        private int _quantiteVendue = 0;
        public double ChiffreDaffaire { get => _chiffreDaffaire; }
        private double _chiffreDaffaire = 0;

        public void AjouterQuantite(int quantite)
        {
            
            _quantiteVendue += quantite;
            
        }

        public void AjouterChiffreDAffaire(double chiffreDaffaire)
        {
            _chiffreDaffaire += chiffreDaffaire;   
        }

    }
}
