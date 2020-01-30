using System;
using System.Collections.Generic;
using System.Text;

namespace Domaine
{
    public class CatalogueQuotidien
    {

        private class CatalogueQuotidienPropreAMagasin
        {
            private Dictionary<uint, double> _produitsVersPrix = new Dictionary<uint, double>();

            public void DefinirLePrixDe(uint produit, double prix)
            {
                this._produitsVersPrix[produit] = prix;
            }

            public double ObtenirLePrixDe(uint produit)
            {
                return this._produitsVersPrix[produit];
            }
        }

        private Dictionary<Guid, CatalogueQuotidienPropreAMagasin> _magasinsVersCataloguesLocaux;

        public CatalogueQuotidien()
        {
            this._magasinsVersCataloguesLocaux = new Dictionary<Guid, CatalogueQuotidienPropreAMagasin>();
        }

        public void DefinirLePrixDe(uint produit, double prix, Guid magasin)
        {
            ObtenirCatalogueDuMagasin(magasin).DefinirLePrixDe(produit, prix);
        }

        public double ObtenirLePrixDe(uint produit, Guid magasin)
        {
            return ObtenirCatalogueDuMagasin(magasin).ObtenirLePrixDe(produit);
        }

        private CatalogueQuotidienPropreAMagasin ObtenirCatalogueDuMagasin(Guid magasin)
        {
            

            if (_magasinsVersCataloguesLocaux.ContainsKey(magasin))
                return _magasinsVersCataloguesLocaux[magasin];

            return (_magasinsVersCataloguesLocaux[magasin] = new CatalogueQuotidienPropreAMagasin());
        }
    }
}
