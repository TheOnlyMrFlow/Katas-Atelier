using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domaine
{
    class StatistiqueMagasin // Entity
    {

        public StatistiqueMagasin(Guid magasin)
        {
            this.magasin = magasin;
        }

        public Guid magasin { get; }

        private readonly object _lock = new object();

        Dictionary<uint, StatistiqueProduit> produitVersStatistiqueLocale = new Dictionary<uint, StatistiqueProduit>();

        public void AjouterQuantiteEtChiffreDaffaireAuProduit(uint idProduit, int quantite, decimal chiffreDaffaire)
        {
            lock (_lock) {
                StatistiqueProduit statLocaleDuProduit;
                if (produitVersStatistiqueLocale.ContainsKey(idProduit))
                {
                    statLocaleDuProduit = produitVersStatistiqueLocale[idProduit];
                }
                else
                {
                    statLocaleDuProduit = new StatistiqueProduit(idProduit);
                    produitVersStatistiqueLocale[idProduit] = statLocaleDuProduit;
                }
                statLocaleDuProduit.AjouterQuantite(quantite);
                statLocaleDuProduit.AjouterChiffreDAffaire(chiffreDaffaire);
            }
        }

        public IEnumerable<StatistiqueProduit> Top100Ventes()
        {
            return produitVersStatistiqueLocale
                .Values
                .OrderByDescending(stat => stat.QuantitéVendue)
                .Take(100);
                //.Select(stat => stat.IdProduit);
        }

        public IEnumerable<StatistiqueProduit> Top100CA()
        {
            return produitVersStatistiqueLocale
                .Values
                .OrderByDescending(stat => stat.ChiffreDAffaire)
                .Take(100);
                //.Select(stat => stat.IdProduit);
        }
    }
}
