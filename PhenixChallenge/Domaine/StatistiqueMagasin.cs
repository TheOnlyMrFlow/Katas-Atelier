using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domaine
{
    class StatistiqueMagasin // Entity
    {

        public StatistiqueMagasin(string uidMagasin)
        {
            this.UidMagasin = uidMagasin;
        }

        public string UidMagasin { get; }

        private readonly object _lock = new object();

        Dictionary<uint, StatistiqueProduit> produitVersStatistiqueLocale = new Dictionary<uint, StatistiqueProduit>();

        public void AjouterQuantiteEtChiffreDaffaireAuProduit(uint idProduit, int quantite, double chiffreDaffaire)
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

        public IEnumerable<uint> Top100Ventes()
        {
            return produitVersStatistiqueLocale
                .Values
                .OrderByDescending(stat => stat.QuantiteVendue)
                .Take(100)
                .Select(stat => stat.IdProduit);
        }

        public IEnumerable<uint> Top100CA()
        {
            return produitVersStatistiqueLocale
                .Values
                .OrderByDescending(stat => stat.ChiffreDaffaire)
                .Take(100)
                .Select(stat => stat.IdProduit);
        }
    }
}
